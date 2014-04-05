namespace WaveNET.Server
{
    public interface IWaveServer
        : IWaveletProvider,
            IWaveletFederationProvider,
            IWaveletFederationListener.IFactory
    {
    }
}