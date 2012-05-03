using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation
{
	public interface IEvaluatingDocOpCursor<T> : IDocOpCursor
	{
		T Finish();
	}
}
