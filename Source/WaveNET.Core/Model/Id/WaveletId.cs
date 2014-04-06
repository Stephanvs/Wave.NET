using System;
using System.Diagnostics.Contracts;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Id
{
    public class WaveletId : IComparable<WaveletId>
    {
        public WaveletId(string domain, string id)
        {
            Preconditions.CheckNotNullOrEmpty(domain, "The parameter 'domain' cannot be null or empty");
            Preconditions.CheckNotNullOrEmpty(id, "The parameter 'id' cannot be null or empty");

            // todo: implement some other stuff: http://code.google.com/p/wave-protocol/source/browse/src/org/waveprotocol/wave/model/id/WaveletId.java
            //if (SimplePrefixEscaper.DEFAULT_ESCAPER.hasEscapeCharacters(domain))
            //{
            //    throw new IllegalArgumentException(
            //        "Domain cannot contain characters that requires escaping: " + domain);
            //}

            //if (!SimplePrefixEscaper.DEFAULT_ESCAPER.isEscapedProperly(IdConstants.TOKEN_SEPARATOR, id))
            //{
            //    throw new IllegalArgumentException("Id is not properly escaped: " + id);
            //}

            Domain = domain;
            Id = id;
        }

        public string Domain { get; private set; }
        public string Id { get; private set; }

        public int CompareTo(WaveletId other)
        {
            int domainCompare = Domain.CompareTo(other.Domain);
            if (domainCompare == 0) return Id.CompareTo(other.Id);
            return domainCompare;
        }

        public string Serialize()
        {
            throw new NotImplementedException();
            // todo: implement Serialize, see: http://code.google.com/p/wave-protocol/source/browse/src/org/waveprotocol/wave/model/id/WaveletId.java
            // return LongIdSerialiser.Instance.SerialiseWaveletId(this);
        }

        /// <summary>
        ///     Creates a WaveletId from a serialized wavelet id.
        /// </summary>
        /// <param name="waveletIdString">a serialized wavelet id</param>
        /// <returns>a WaveletId</returns>
        public static WaveletId Deserialize(String waveletIdString)
        {
            throw new NotImplementedException();
            // Todo: implement Deserialize
            //return LongIdSerialiser.Instance.DeserialiseWaveletId(waveletIdString);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (obj is WaveletId)
            {
                var other = (WaveletId) obj;
                if (!Domain.Equals(other.Domain)) return false;
                if (!Id.Equals(other.Id)) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime*result + ((Domain == null) ? 0 : Domain.GetHashCode());
            result = prime*result + ((Id == null) ? 0 : Id.GetHashCode());
            return result;
        }

        public override string ToString()
        {
            return String.Format("[WaveletId:{0}]", Serialize());
        }
    }
}