using System;

namespace WaveNET.Core.Model.Document.Operation
{
    public class BufferedDocInitialization
        : AbstractBufferedDocInitialization
    {
        private readonly IDocOp op;

        public BufferedDocInitialization(IDocOp op)
        {
            this.op = op;
        }

        public override int Size()
        {
            return op.Size();
        }

        public override DocOpComponentType GetType(int i)
        {
            DocOpComponentType t = op.GetType(i);
            if (t is DocInitializationComponentType)
            {
                return t;
            }
            throw new NotSupportedException("Initialization with unexpected component " + t + ": " + op);
        }

        public override void ApplyComponent(int i, IDocOpCursor cursor)
        {
            op.ApplyComponent(i, InitializationCursorAdapter.Adapt(cursor));
        }

        public override IAnnotationBoundaryMap GetAnnotationBoundary(int i)
        {
            return op.GetAnnotationBoundary(i);
        }

        public override string GetCharactersString(int i)
        {
            return op.GetCharactersString(i);
        }

        public override string GetElementStartTag(int i)
        {
            return op.GetElementStartTag(i);
        }

        public override IAttributes GetElementStartAttributes(int i)
        {
            return op.GetElementStartAttributes(i);
        }

        public override void Apply(IDocInitializationCursor cursor)
        {
            op.Apply(InitializationCursorAdapter.Adapt(cursor));
        }

        public override void ApplyComponent(int i, IDocInitializationCursor cursor)
        {
            op.ApplyComponent(i, InitializationCursorAdapter.Adapt(cursor));
        }

        public override DocInitializationComponentType GetComponentType(int i)
        {
            DocOpComponentType t = op.GetType(i);
            if (t is DocInitializationComponentType)
            {
                return (DocInitializationComponentType) t;
            }
            throw new InvalidOperationException(
                "Initialization with unexpected component " + t + ": " + op);
        }
    }
}