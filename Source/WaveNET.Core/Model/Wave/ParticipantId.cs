using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace WaveNET.Core.Model.Wave
{
	/// <summary>
	/// A ParticipantId uniquely identifies a <see cref="Participant"/>. 
	/// It looks like an e-mail address, eg. 'joe@domain.com'
	/// </summary>
	public sealed class ParticipantId
	{
		private string _address;

		/// <summary>
		/// Creates a new instance of the ParticipantId class
		/// </summary>
		/// <param name="address">A non-null adress string</param>
		public ParticipantId(string address)
		{
			Contract.Requires(!String.IsNullOrEmpty(address), "The parameter 'address' cannot be null or empty.");

			_address = Normalize(address);
		}

		/// <summary>
		/// Gets the participant's address
		/// </summary>
		/// <returns>The participant's address</returns>
		public string GetAddress()
		{
			return _address;
		}

		/// <summary>
		/// Gets the domain name in the address. If no '@' occurs, it will be the entire string,
		/// If more than one '@' occurs, it will be the part after the last '@'.
		/// </summary>
		/// <returns></returns>
		public string GetDomain()
		{
			string[] parts = _address.Split('@');
			return parts[parts.Length - 1];
		}

		public override bool Equals(object obj)
		{
			if (obj == this) return true;
			else if (obj is ParticipantId) return ((ParticipantId)obj)._address.Equals(this._address);

			return false;
		}

		public override int GetHashCode()
		{
			return _address.GetHashCode();
		}

		public override string ToString()
		{
			return _address;
		}

		/// <summary>
		/// Normalizes an address.
		/// </summary>
		/// <param name="address">Address to normalize, can be null</param>
		/// <returns>Normal form of <paramref name="address"/> if not null; null otherwise.</returns>
		private static string Normalize(string address)
		{
			if (address == null) return null;

			return address.ToLower();
		}
	}
}
