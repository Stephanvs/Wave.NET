using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Operation
{
	[global::System.Serializable]
	public class TransformException : Exception
	{
		public TransformException() { }
		public TransformException(string message) : base(message) { }
		public TransformException(string message, Exception inner) : base(message, inner) { }
		
		protected TransformException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
