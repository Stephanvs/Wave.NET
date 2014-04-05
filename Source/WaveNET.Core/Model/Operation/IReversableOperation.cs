using System.Collections.Generic;

namespace WaveNET.Core.Model.Operation
{
    /// <summary>
    ///     This represents an operation that is able to return
    ///     the reverse operation of itself after application.
    /// </summary>
    /// <typeparam name="TOperation">The Operation</typeparam>
    /// <typeparam name="TTarget">The type on which Apply() and ApplyAndReturnReverse() can be called</typeparam>
    public interface IReversableOperation<TOperation, in TTarget>
        : IOperation<TTarget> where TOperation : IOperation<TTarget>
    {
        /// <summary>
        ///     Applies the operation to a target and returns a sequence of operations
        ///     which can reverse the application.
        /// </summary>
        /// <param name="target">The target onto which to apply the operation</param>
        /// <returns>A sequence of operations that reverses the application of this operation.</returns>
        /// <remarks>
        ///     The returned sequence of operations, when applied in order after this operation is applied, should reverse the
        ///     effect of this operation
        /// </remarks>
        IList<TOperation> ApplyAndReturnReverse(TTarget target);
    }
}