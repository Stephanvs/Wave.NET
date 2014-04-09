using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation
{
    public abstract class AbstractDocInitialization
        : IDocInitialization
    {
        void IOperation<IDocOpCursor>.Apply(IDocOpCursor target)
        {
            Apply(target);
        }

        public abstract void Apply(IDocOpCursor cursor);
        public abstract void Apply(IDocInitializationCursor cursor);
        public abstract void ApplyComponent(int i, IDocInitializationCursor cursor);
        public abstract DocInitializationComponentType GetComponentType(int i);
    }
}