using System.Collections.Generic;

namespace WaveNET.Core.Model.Document.Operation
{
    public class DocInitializationBuilder
    {
        private readonly List<DocOpComponent> _accu = new List<DocOpComponent>();

        public IDocInitialization Build()
        {
            // Todo: This should not need to call AsInitialization()
            return DocOpUtil.AsInitialization(BufferedDocOp.Create(_accu));
        }
    }
}