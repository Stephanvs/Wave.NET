using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Document.Operation.Util;

namespace WaveNET.Core.Model.Document.Operation
{
	/// <summary>
	/// A mutation of an <code>IAttributes</code> object. This is a map from attribute
	/// names to attribute values, indicating the attributes whose values should be
	/// changed and what values they should be assigned, where a null value indicates
	/// that the corresponding attribute should be removed. The entries in the map
	/// are sorted by the attribute names, so that the set obtained from entrySet()
	/// will allow you to easily iterate through the entries in sorted order.
	///
	/// Implementations must be immutable.
	/// </summary>
	public interface IAttributesUpdate : IUpdateMap
	{
		IAttributesUpdate ComposeWith(IAttributesUpdate mutation);
		IAttributesUpdate Exclude(ICollection<String> keys);
	}
}
