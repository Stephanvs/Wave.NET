using FluentAssertions;
using WaveNET.Core.Model.Wave;
using Xunit;

namespace WaveNET.Tests.Core.Models.Ids
{
    public class ParticipantIdTests
    {
        private static ParticipantId CreateParticipantId(string address)
        {
            return new ParticipantId(address);
        }

        [Fact]
        public void TypicalAddressIsValid()
        {
            var p = CreateParticipantId("test@example.com");

            p.Domain.Should().Be("example.com");
        }

        [Fact]
        public void DomainOnlyIsValid()
        {
            CreateParticipantId("@example.com");
        }
    }
}