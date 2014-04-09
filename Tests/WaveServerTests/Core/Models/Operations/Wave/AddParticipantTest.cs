using System;
using System.Collections;
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
        private static readonly ParticipantId Creator = new ParticipantId("abc@example.com");
        private static readonly ParticipantId Another = new ParticipantId("def@example.com");
        private static readonly ParticipantId AThird = new ParticipantId("xyz@example.com");

        [Fact]
        public void ReverseOfAddParticipantIsRemoveParticipant()
        {
            var context = A.Fake<WaveletOperationContext>();
            var rop = new RemoveParticipantOperation(context, Creator);
            var aop = new AddParticipantOperation(context, Creator, 0);
            var wavelet = CreateWaveletData();

            var reverse = aop.ApplyAndReturnReverse(wavelet);
            var result = reverse[0];

            reverse.Should().HaveCount(1);
            result.Should().BeOfType<RemoveParticipantOperation>();
            result.IsWorthyOfAttribution().Should().BeTrue();
            result.As<RemoveParticipantOperation>().ParticipantId.Should().Be(Creator);
        }

        [Fact]
        public void ReverseOfRemoveParticipantIsAddParticipantWithPosition()
        {
            throw new NotImplementedException();
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