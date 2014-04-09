using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FakeItEasy;
using FluentAssertions;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Operation;
using WaveNET.Core.Model.Operation.Wave;
using WaveNET.Core.Model.Version;
using WaveNET.Core.Model.Wave;
using WaveNET.Core.Model.Wave.Data;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Wave
{
    public class AddParticipantTest
    {
        private static readonly ParticipantId Creator = new ParticipantId("creator@example.com");
        private static readonly ParticipantId Another = new ParticipantId("another@example.com");
        private static readonly ParticipantId AThird = new ParticipantId("athird@example.com");

        [Fact]
        public void ReverseOfAddParticipantIsRemoveParticipant()
        {
            var context = A.Fake<WaveletOperationContext>();
            var rop = new RemoveParticipantOperation(context, Creator);
            var aop = new AddParticipantOperation(context, Creator, 0);
            WaveletData wavelet = CreateWaveletData();

            IList<WaveletOperation> reverse = aop.ApplyAndReturnReverse(wavelet);
            WaveletOperation result = reverse[0];

            reverse.Should().HaveCount(1);
            result.Should().BeOfType<RemoveParticipantOperation>();
            result.IsWorthyOfAttribution().Should().BeTrue();
            result.As<RemoveParticipantOperation>().ParticipantId.Should().Be(Creator);
        }

        [Fact]
        public void ReverseOfRemoveParticipantIsAddParticipantWithPosition()
        {
            var wavelet = CreateWaveletData();
            var context = A.Fake<WaveletOperationContext>();

            // Build participant list with 3 participants.
            var participants = new List<ParticipantId> { Creator, Another, AThird };
            foreach (var p in participants)
            {
                wavelet.AddParticipant(p);
            }

            // Assert that all participants match the ones we just created
            wavelet.GetParticipants().Should().Contain(participants);

            // The reverse of removing any of the participants is an AddParticipantOperation with the
            // correct position which, when applied, rolls back the participant list.
            for (var i = 0; i < participants.Count; i++)
            {
                // Cache current participant
                var participant = participants[i];

                // Get and apply the exact reverse of the RemoveParticipantOperation (should yield an AddParticipantOperation)
                var reverse = new RemoveParticipantOperation(context, participant).ApplyAndReturnReverse(wavelet);

                reverse.Should().HaveCount(1);
                reverse.Should().Contain(new AddParticipantOperation(context, participant, i));

                // Re-apply the reverse (thus essentially re-adding the just removed participant)
                reverse[0].Apply(wavelet);

                // Assert we're back in original state
                wavelet.GetParticipants().Should().Contain(participants);
            }
        }

        [Fact]
        public void CanAddTwoDifferentParticipants()
        {
            WaveId waveId = WaveId.Of("example.com", "c+123");
            WaveletId waveletId = WaveletId.Of("example.com", IdConstants.ConversationRootWavelet);

            var wavelet = new WaveletData(waveletId, Creator, DateTime.UtcNow.AddMinutes(-1), 1, new HashedVersion(),
                DateTime.UtcNow, waveId);

            var context = A.Fake<WaveletOperationContext>();

            new AddParticipantOperation(context, Creator, 0).Apply(wavelet);
            new AddParticipantOperation(context, Another, 0).Apply(wavelet);

            ReadOnlyCollection<ParticipantId> participants = wavelet.GetParticipants();

            participants.Should().HaveCount(2);
            participants.Should().Contain(Creator);
            participants.Should().Contain(Another);
        }

        [Fact]
        public void CannotAddSameParticipantTwice()
        {
            WaveletData wavelet = CreateWaveletData();
            var context = A.Fake<WaveletOperationContext>();
            var first = new AddParticipantOperation(context, Creator, 0);
            var second = new AddParticipantOperation(context, Creator, 0);

            first.Apply(wavelet);
            var ex = Assert.Throws<OperationException>(() => second.Apply(wavelet));

            ex.Message.Should().Be("Attempt to add a duplicate participant");
        }

        private static WaveletData CreateWaveletData()
        {
            WaveId waveId = WaveId.Of("example.com", "c+123");
            WaveletId waveletId = WaveletId.Of("example.com", IdConstants.ConversationRootWavelet);

            return new WaveletData(waveletId, Creator, DateTime.UtcNow.AddMinutes(-1), 1, new HashedVersion(),
                DateTime.UtcNow, waveId);
        }
    }
}