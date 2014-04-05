namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     A superset of the DocInitializationComponentType enum that adds
    ///     update and deletion components.
    /// </summary>
    /// <seealso
    ///     cref="http://code.google.com/p/wave-protocol/source/browse/src/org/waveprotocol/wave/model/document/operation/DocOpComponentType.java" />
    public class DocOpComponentType
    {
        public static DocInitializationComponentType AnnotationBoundary =
            new DocInitializationComponentType("annotation boundary");

        public static DocInitializationComponentType Characters = new DocInitializationComponentType("characters");
        public static DocInitializationComponentType ElementStart = new DocInitializationComponentType("element start");
        public static DocInitializationComponentType ElementEnd = new DocInitializationComponentType("element end");

        public static DocOpComponentType Retain = new DocOpComponentType("retain");
        public static DocOpComponentType DeleteCharacters = new DocOpComponentType("delete characters");
        public static DocOpComponentType DeleteElementStart = new DocOpComponentType("delete element start");
        public static DocOpComponentType DeleteElementEnd = new DocOpComponentType("delete element end");
        public static DocOpComponentType ReplaceAttributes = new DocOpComponentType("replace attributes");
        public static DocOpComponentType UpdateAttributes = new DocOpComponentType("update attributes");

        protected string _name;

        public DocOpComponentType(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}