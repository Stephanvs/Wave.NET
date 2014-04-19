using System;
using FakeItEasy;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Version;
using WaveNET.Core.Model.Wave;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Tests.Testing
{
    public class WaveletDataFactory<T>
        where T : IWaveletData
    {
        private readonly WaveletData.WaveletDataFactory _factory;
        private readonly ParticipantId _participantId = new ParticipantId("fake@example.com");
        private static WaveId _waveId;
        private static WaveletId _waveletId;

        static WaveletDataFactory()
        {
            var gen = A.Fake<IIdGenerator>();
            _waveId = gen.NewWaveId();
            _waveletId = gen.NewConversationWaveletId();
        } 

        private WaveletDataFactory(WaveletData.WaveletDataFactory factory)
        {
            _factory = factory;
        }

        public static WaveletDataFactory<T> Of(WaveletData.WaveletDataFactory factory)
        {
            return new WaveletDataFactory<T>(factory);
        }

        public IWaveletData Create()
        {
            return _factory.Create(new EmptyWaveletSnapshot(_waveId, _waveletId, _participantId,
                HashedVersion.Unsigned(0), DateTime.UtcNow));
        }
    }
}