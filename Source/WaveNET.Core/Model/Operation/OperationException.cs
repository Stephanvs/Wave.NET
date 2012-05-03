using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Operation
{
	[global::System.Serializable]
	public class OperationException : Exception
	{
		public OperationException() { }
		public OperationException(string message) : base(message) { }
		public OperationException(string message, Exception inner) : base(message, inner) { }
		
		protected OperationException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
