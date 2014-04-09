using WaveNET.Core.Model.Document.Operation;

namespace WaveNET.Core.Model.Document.Util
{
    public class EmptyDocument
    {
         public static readonly IDocInitialization Empty = new DocInitializationBuilder().Build();
    }
}