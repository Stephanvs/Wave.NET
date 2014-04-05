using System.Collections.Generic;
using WaveNET.Core.Model.Id;

namespace WaveNET.Server
{
    public interface IWaveletProvider
    {
        void SetListener(IWaveletListener listener);
        void SubmitRequest(WaveletName waveletName, ProtocolWaveletDelta delta, SubmitResultListener listener);

        IList<ProtocolWaveletDelta> RequestHistory(WaveletName waveletName, ProtocolHashedVersion versionStart,
                                                   ProtocolHashedVersion versionEnd);
    }
}