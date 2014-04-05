using System;
using System.Collections.Generic;

namespace WaveNET.Server
{
    /// <summary>
    ///     Interface that's used by the WaveView classes to listen to updates on wavelets.
    /// </summary>
    public interface IWaveletListener
    {
        void WaveletUpdate(WaveletName waveletName, List<ProtocolWaveletDelta> newDeltas,
                           ProtocolHashedVersion resultingVersion, Dictionary<String, BufferedDocOp> documentState);

        void WaveletCommitted(WaveletName waveletName, ProtocolHashedVersion version);
    }
}