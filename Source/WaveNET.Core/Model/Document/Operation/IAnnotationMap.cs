using WaveNET.Core.Model.Document.Operation.Util;

namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     Implementations must be immutable.
    /// </summary>
    public interface IAnnotationMap : IStateMap
    {
        /// <summary>
        ///     Returns a new map, updated with the differences
        /// </summary>
        /// <param name="mutation">The mutation must be compatible with the current map</param>
        /// <returns>A new immutable map</returns>
        IAnnotationMap UpdateWith(IAnnotationsUpdate mutation);

        /// <summary>
        ///     Same as <see cref="UpdateWith(AnnotationsUpdate)" />, but the mutation does
        ///     not have to be compatible. This is useful when the mutation may
        ///     already be known to be incompatible, but we wish still to perform it.
        ///     This is useful for validity checking code, for example. In general,
        ///     <see cref="UpdateWith(AnnotationsUpdate)" /> should be used instead.
        /// </summary>
        /// <param name="mutation">Does not have to be compatible</param>
        /// <returns>a new updated map, based on the key to new-value pairs, ignoring the old values.</returns>
        IAnnotationMap UpdateWithNoCompatibilityCheck(IAnnotationsUpdate mutation);
    }
}