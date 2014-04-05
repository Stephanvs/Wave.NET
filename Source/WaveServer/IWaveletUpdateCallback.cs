namespace WaveNET.Server
{
    public interface IWaveletUpdateCallback
    {
        void OnSuccess();
        void OnFailure(string errorMessage);
    }
}