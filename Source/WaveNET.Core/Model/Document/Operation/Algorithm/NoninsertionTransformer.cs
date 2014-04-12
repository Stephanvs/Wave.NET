using System;
using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class NoninsertionTransformer
    {
        /// <summary>
        /// Transforms a pair of insertion-free operations.
        /// </summary>
        /// <param name="clientOperation">the operation from the client</param>
        /// <param name="serverOperation">the operation from the server</param>
        /// <returns>the transformed pair of operations</returns>
        public OperationPair<IDocOp> TransformOperations(IDocOp clientOperation, IDocOp serverOperation)
        {
            throw new NotImplementedException();
        }
    }
}