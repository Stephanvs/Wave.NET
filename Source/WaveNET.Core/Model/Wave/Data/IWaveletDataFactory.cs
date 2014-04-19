namespace WaveNET.Core.Model.Wave.Data
{
    public interface IWaveletDataFactory<out T>
        : IReadableWaveletDataFactory<T>
        where T : IWaveletData
    {
    }
}