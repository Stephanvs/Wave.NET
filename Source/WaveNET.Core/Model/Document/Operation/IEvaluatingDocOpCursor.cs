using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation
{
	public interface IEvaluatingDocOpCursor<out T> : IDocOpCursor
	{
		T Finish();
	}
}
