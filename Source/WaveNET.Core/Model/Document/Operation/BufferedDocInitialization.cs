using System;

namespace WaveNET.Core.Model.Document.Operation
{
    public class BufferedDocInitialization
        : AbstractBufferedDocInitialization
    {
        private readonly IDocOp _op;

        public BufferedDocInitialization(IDocOp op)
        {
            this._op = op;
        }

        public override int Size()
        {
            return _op.Size();
        }

        public override DocOpComponentType GetType(int i)
        {
            DocOpComponentType t = _op.GetType(i);
            if (t is DocInitializationComponentType)
            {
                return t;
            }
            throw new NotSupportedException("Initialization with unexpected component " + t + ": " + _op);
        }

        public override void ApplyComponent(int i, IDocOpCursor cursor)
        {
            _op.ApplyComponent(i, InitializationCursorAdapter.Adapt(cursor));
        }

        public override IAnnotationBoundaryMap GetAnnotationBoundary(int i)
        {
            return _op.GetAnnotationBoundary(i);
        }

        public override string GetCharactersString(int i)
        {
            return _op.GetCharactersString(i);
        }

        public override string GetElementStartTag(int i)
        {
            return _op.GetElementStartTag(i);
        }

        public override IAttributes GetElementStartAttributes(int i)
        {
            return _op.GetElementStartAttributes(i);
        } 

        public override void Apply(IDocInitializationCursor cursor)
        {
            _op.Apply(InitializationCursorAdapter.Adapt(cursor));
        }

        public override void ApplyComponent(int i, IDocInitializationCursor cursor)
        {
            _op.ApplyComponent(i, InitializationCursorAdapter.Adapt(cursor));
        }

        public override DocInitializationComponentType GetComponentType(int i)
        {
            DocOpComponentType t = _op.GetType(i);
            if (t is DocInitializationComponentType)
            {
                return (DocInitializationComponentType) t;
            }
            throw new InvalidOperationException(
                "Initialization with unexpected component " + t + ": " + _op);
        }
    }
}