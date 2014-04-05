namespace WaveNET.Server
{
    public interface IFactory
    {
        IWaveletFederationListener ListenerForDomain(string domain);
    }
}