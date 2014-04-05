using System;

namespace WaveNET.Server
{
    public interface IPostSignerInfoResponseListener
    {
        void OnSuccess();
        void OnFailure(String errorMessage);
    }
}