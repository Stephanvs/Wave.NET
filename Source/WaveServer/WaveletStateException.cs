using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Server
{
	[global::System.Serializable]
	public class WaveletStateException : Exception
	{
		public WaveletStateException() { }
		public WaveletStateException(string message) : base(message) { }
		public WaveletStateException(string message, Exception inner) : base(message, inner) { }

		protected WaveletStateException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
