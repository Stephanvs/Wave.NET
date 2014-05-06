namespace WaveNET.Core.Model.Document.Operation.Automaton
{
    public class DocumentSchema
    {
        public static readonly IDocumentSchema NoSchemaConstraints = new NoSchemaConstraintsDocumentSchema();
    }

    public class NoSchemaConstraintsDocumentSchema : IDocumentSchema
    {
    }
}