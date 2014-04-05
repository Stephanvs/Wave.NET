using System.Collections.Generic;
using WaveNET.Core.Model.Id;

namespace WaveNET.Server
{
    public interface IWaveletFederationListener
    {
        void WaveletDeltaUpdate(WaveletName waveletName, List<string> deltas, IWaveletUpdateCallback callback);

        void WaveletCommitUpdate(WaveletName waveletName, ProtocolHashedVersion committedVersion,
                                 IWaveletUpdateCallback callback);
    }
}