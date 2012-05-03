using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Operation
{
	public interface IOperation<T>
	{
		/// <summary>
		/// Applies this operation to the given target
		/// </summary>
		/// <param name="target">target on which this operation applies itself</param>
		void Apply(T target);
	}
}
