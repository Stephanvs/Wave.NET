using System.Collections.Generic;
using System.Linq;
using WaveNET.Core.Model.Document.Operation.Util;

namespace WaveNET.Core.Model.Document.Operation
{
    public class AnnotationsUpdate
        : ImmutableUpdateMap<AnnotationsUpdate, IAnnotationsUpdate>
        , IAnnotationsUpdate
    {
        public AnnotationsUpdate() { }

        private AnnotationsUpdate(IList<AttributeUpdate> updates) 
            : base(updates) { }

        public IAnnotationsUpdate ComposeWith(IAnnotationsUpdate mutation)
        {
            throw new System.NotImplementedException();
        }

        public IAnnotationsUpdate ComposeWith(IAnnotationBoundaryMap map)
        {
            throw new System.NotImplementedException();
        }
    }
}