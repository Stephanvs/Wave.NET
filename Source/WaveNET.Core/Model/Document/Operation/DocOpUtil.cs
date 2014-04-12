using System;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation
{
    public class DocOpUtil
    {
        internal static string ToConciseString(IDocOp docOp)
        {
            var sb = new StringBuilder();
            docOp.Apply(CreateConciseStringBuilder(docOp, sb));
            return sb.ToString();
        }

        private static IDocOpCursor CreateConciseStringBuilder(IDocOp docOp, StringBuilder sb)
        {
            throw new NotImplementedException();
        }

        public static IDocInitialization AsInitialization(IBufferedDocOp bufferedDocOp)
        {
            if (bufferedDocOp is IDocInitialization)
            {
                return (IDocInitialization) bufferedDocOp;
            }
            return new BufferedDocInitialization(bufferedDocOp);
        }
    }
}