namespace WaveNET.Core.Model.Wave.Data
{
    public interface IReadableWaveletDataFactory<out T>

        where T: IReadableWaveletData
    {
        T Create(IReadableWaveletData data);
    }
}