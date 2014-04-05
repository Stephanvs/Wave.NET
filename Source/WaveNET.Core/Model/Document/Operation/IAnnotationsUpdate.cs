using WaveNET.Core.Model.Document.Operation.Util;

namespace WaveNET.Core.Model.Document.Operation
{
    public interface IAnnotationsUpdate : IUpdateMap
    {
        IAnnotationsUpdate ComposeWith(IAnnotationsUpdate mutation);
        IAnnotationsUpdate ComposeWith(IAnnotationBoundaryMap map);
    }
}