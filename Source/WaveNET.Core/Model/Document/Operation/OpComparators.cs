using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation
{
	/// <summary>
	/// Utilities for comparing operations.
	/// </summary>
	public class OpComparators : IEqualityComparer<IDocOp>
	{
		private  OpComparators() { }

		public bool Equals(IDocOp x, IDocOp y)
		{
			if (x == null) return y == null;
			if (y == null) return false;

			// TODO: Comparing by stringifying is unnecessarily expensive.
			return DocOpUtil.ToConciseString(x).Equals(DocOpUtil.ToConciseString(y));
		}

		public int GetHashCode(IDocOp obj)
		{
			throw new NotImplementedException();
		}
	}
}
