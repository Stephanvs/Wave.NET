using System;
using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class Composer
    {
        private Composer(IEvaluatingDocOpCursor<IDocOp> cursor)
        {
            // TODO: Implement - copied from java source
            //normalizer = OperationNormalizer.createNormalizer(cursor);
        }

        private IDocOp ComposeOperations(IDocOp first, IDocOp second)
        {
            throw new NotImplementedException();
        }

        public static IDocOp Compose(IDocOp first, IDocOp second)
        {
            try
            {
                return new Composer(new DocOpBuffer()).ComposeOperations(first, second);
            }
            catch (ComposeException exception)
            {
                throw new OperationException(exception.Message);
            }
        }
    }
}