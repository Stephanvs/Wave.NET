namespace WaveNET.Core.Model.Id
{
    public interface IIdSerializer
    {
        /// <summary>
        ///     Turn a wave id into a string. If the domain of the id is the same as this,
        ///     then the serialised form does not contain the domain.
        /// </summary>
        string SerializeWaveId(WaveId waveId);

        /// <summary>
        ///     Turn a wavelet id into a string. If the domain of the id is the same as this,
        ///     then the serialised form does not contain the domain.
        /// </summary>
        string SerializeWaveletId(WaveletId waveletId);

        /// <summary>
        ///     Turn a string into a wavel id. If the domain is not specified in the string, then the
        ///     wave id returned has the default domain.
        /// </summary>
        WaveId DeserializeWaveId(string serializedForm);

        /// <summary>
        ///     Turn a string into a wavel id. If the domain is not specified in the string, then the
        ///     wave id returned has the default domain.
        /// </summary>
        WaveletId DeserializeWaveletId(string serializedForm);
    }
}