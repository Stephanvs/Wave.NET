using System.Globalization;
using System.Text;

namespace WaveNET.Core.Model.Id.Serializers
{
    public class ModernIdSerializer
        : IIdSerializer
    {
        private const char SEP = '/';
        private const char ELIDE = '~';
        public static readonly ModernIdSerializer Instance = new ModernIdSerializer();

        private ModernIdSerializer()
        {
        }

        public string SerializeWaveId(WaveId waveId)
        {
            return new StringBuilder(waveId.Domain).Append(SEP).Append(waveId.Id).ToString();
        }

        public string SerializeWaveletId(WaveletId waveletId)
        {
            return new StringBuilder(waveletId.Domain).Append(SEP).Append(waveletId.Id).ToString();
        }

        public string SerializeWaveletName(WaveletName name)
        {
            StringBuilder b = new StringBuilder(SerializeWaveId(name._waveId)).Append(SEP);
            b.Append(name._waveletId.Domain.Equals(name._waveId.Domain)
                ? ELIDE.ToString(CultureInfo.InvariantCulture)
                : name._waveletId.Domain);
            b.Append(SEP).Append(name._waveletId.Id);
            return b.ToString();
        }

        public WaveId DeserializeWaveId(string serializedForm)
        {
            string[] tokens = serializedForm.Split(new[] {SEP});
            if (tokens.Length != 2)
                throw new InvalidIdException(
                    "Required 2 '/'-separated tokens in serialzed wave i: " + serializedForm);

            return WaveId.Of(tokens[0], tokens[1]);
        }

        public WaveletId DeserializeWaveletId(string serializedForm)
        {
            string[] tokens = serializedForm.Split(new[] {SEP});
            if (tokens.Length != 2)
                throw new InvalidIdException(
                    "Required 2 '/'-separated tokens in serialzed wave i: " + serializedForm);

            return WaveletId.Of(tokens[0], tokens[1]);
        }

        public WaveletName DeserializeWaveletName(string serializedForm)
        {
            string[] tokens = serializedForm.Split(new[] {SEP});
            if (tokens.Length != 4)
                throw new InvalidIdException(
                    "Required 4 '/'-separated tokens in serialzed wave i: " + serializedForm);

            if (tokens[2].Equals(tokens[0]))
            {
                throw new InvalidIdException(
                    "Serialized wavelet name had un-normalized domains");
            }

            if (tokens[2].Equals(ELIDE.ToString(CultureInfo.InvariantCulture)))
            {
                tokens[2] = tokens[0];
            }

            return WaveletName.Of(
                WaveId.Of(tokens[0], tokens[1]),
                WaveletId.Of(tokens[2], tokens[3]));
        }
    }
}