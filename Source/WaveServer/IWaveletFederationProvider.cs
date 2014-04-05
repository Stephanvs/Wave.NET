using System;
using WaveNET.Core.Model.Id;

namespace WaveNET.Server
{
    public interface IWaveletFederationProvider
    {
        void SubmitRequest(WaveletName waveletName, ProtocolSignedDelta delta, SubmitResultListener listener);

        void RequestHistory(WaveletName waveletName, String domain, ProtocolHashedVersion startVersion,
                            ProtocolHashedVersion endVersion, long lengthLimit, IHistoryResponseListener listener);

        void GetDeltaSignerInfo(String signerId, WaveletName waveletName, ProtocolHashedVersion deltaEndVersion,
                                IDeltaSignerInfoResponseListener listener);

        void PostSignerInfo(String destinationDomain, ProtocolSignerInfo signerInfo,
                            IPostSignerInfoResponseListener listener);
    }
}