using System;
using WaveNET.Core.Model.Id.Serializers;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Id
{
    /// <summary>
    ///     A wave is identified by a tuple of a wave provider domain and a local
    ///     identifier which is unique within the domain.
    /// </summary>
    public sealed class WaveId : IComparable<WaveId>
    {
        private readonly IIdSerializer _idSerializer = ModernIdSerializer.Instance;

        /// <summary>
        ///     Creates an instance of a WaveId class.
        /// </summary>
        /// <param name="domain">Domain must not be null. This is assumed to be of a valid canonical domain format.</param>
        /// <param name="id">Id must not be null. This is assumed to be escaped with SimplePrefixEscaper.DefaultEscaper.</param>
        private WaveId(string domain, string id)
        {
            Preconditions.CheckNotNullOrEmpty(domain, "The parameter 'domain' cannot be null");
            Preconditions.CheckNotNullOrEmpty(id, "The parameter 'id' cannot be null");

            // todo: some stuff here, see: http://code.google.com/p/wave-protocol/source/browse/src/org/waveprotocol/wave/model/id/WaveId.java
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

        public int CompareTo(WaveId other)
        {
            int domainCompare = Domain.CompareTo(other.Domain);

            if (domainCompare == 0) return Id.CompareTo(other.Id);
            return domainCompare;
        }

        public static WaveId Of(string domain, string id)
        {
            return new WaveId(domain, id);
        }

        /// <summary>
        ///     Serialises this waveId into a unique string. For any two wave ids, waveId1.serialise().equals(waveId2.serialise())
        ///     iff waveId1.equals(waveId2).
        /// </summary>
        /// <returns></returns>
        public String Serialize()
        {
            return _idSerializer.SerializeWaveId(this);
        }

        public static WaveId Deserialize(string serializedForm)
        {
            return ModernIdSerializer.Instance.DeserializeWaveId(serializedForm);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (obj is WaveId)
            {
                var other = (WaveId) obj;
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
            return String.Format("[WaveId {0}]", Serialize());
        }
    }
}