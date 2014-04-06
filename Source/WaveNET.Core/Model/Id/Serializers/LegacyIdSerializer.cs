using System;
using WaveNET.Core.Model.Id;

namespace WaveNET.Core.Model.Id.Serializers
{
    [Obsolete("Use ModernIdSerializer instead", false)]
    public class LegacyIdSerializer
        : IIdSerializer
    {
        private const string PartSeparator = "!";

        public string SerializeWaveId(WaveId waveId)
        {
            return waveId.Domain + PartSeparator + waveId.Id;
        }

        public string SerializeWaveletId(WaveletId waveletId)
        {
            return waveletId.Domain + PartSeparator + waveletId.Id;
        }

        public string SerializeWaveletName(WaveletName name)
        {
            throw new NotImplementedException("No legacy serializer available for wavelet names");
        }

        public WaveId DeserializeWaveId(string serializedForm)
        {
            throw new System.NotImplementedException();
        }

        public WaveletId DeserializeWaveletId(string serializedForm)
        {
            throw new System.NotImplementedException();
        }

        public WaveletName DeserializeWaveletName(string serializedForm)
        {
            throw new NotImplementedException("No legacy serialization for wavelet names");
        }
    }
}