using System;

namespace WaveNET.Server
{
    public interface IDeltaSignerInfoResponseListener
    {
        void OnSuccess(ProtocolSignerInfo signerInfo);
        void OnFailure(String errorMessage);
    }
}