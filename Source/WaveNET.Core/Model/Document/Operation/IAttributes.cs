using System.Collections;
using System.Collections.Generic;

namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     A set of attributes, implemented as a map from attribute names to attribute
    ///     values. The attributes in the map are sorted by their names, so that the set
    ///     obtained from entrySet() will allow you to easily iterate through the
    ///     attributes in sorted order.
    ///     Implementations must be immutable.
    /// </summary>
    public interface IAttributes
        : IDictionary<string, string>
    {
        IAttributes UpdateWith(IAttributesUpdate mutation);

        /// <summary>
        ///     Same as <see cref="UpdateWith" />, but the mutation does
        ///     not have to be compatible. This is useful when the mutation may
        ///     already be known to be incompatible, but we wish still to perform it.
        ///     This is useful for validity checking code, for example. In general,
        ///     <see cref="UpdateWith" /> should be used instead.
        /// </summary>
        /// <param name="mutation">does not have to be compatible</param>
        /// <returns>a new updated map, based on the key to new-value pairs, ignoring the old values.</returns>
        IAttributes UpdateWithNoCompatibilityCheck(IAttributesUpdate mutation);
    }
}