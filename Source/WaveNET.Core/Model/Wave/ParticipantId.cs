using System;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Wave
{
    /// <summary>
    ///     A ParticipantId uniquely identifies a <see cref="Participant" />.
    ///     It looks like an e-mail address, eg. 'joe@domain.com'
    /// </summary>
    public sealed class ParticipantId
    {
        /// <summary>
        ///     Creates a new instance of the ParticipantId class
        /// </summary>
        /// <param name="address">A non-null adress string</param>
        public ParticipantId(string address)
        {
            Preconditions.CheckNotNullOrEmpty(address, "The parameter 'address' cannot be null or empty.");

            Address = Normalize(address);
        }

        /// <summary>
        ///     Gets the participant's address
        /// </summary>
        /// <value>The participant&apos;s address</value>
        public string Address { get; private set; }

        /// <summary>
        ///     Gets the domain name in the address. If no '@' occurs, it will be the entire string,
        ///     If more than one '@' occurs, it will be the part after the last '@'.
        /// </summary>
        /// <value></value>
        public string Domain
        {
            get
            {
                string[] parts = Address.Split('@');
                return parts[parts.Length - 1];
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            var id = obj as ParticipantId;
            if (id != null) return id.Address.Equals(Address);

            return false;
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }

        public override string ToString()
        {
            return Address;
        }

        /// <summary>
        ///     Normalizes an address.
        /// </summary>
        /// <param name="address">Address to normalize, can be null</param>
        /// <returns>Normal form of <paramref name="address" /> if not null; null otherwise.</returns>
        private static string Normalize(string address)
        {
            return address == null
                ? null
                : address.ToLower();
        }
    }
}