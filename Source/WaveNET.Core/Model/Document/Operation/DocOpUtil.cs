using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation
{
    public static class DocOpUtil
    {
        internal static string ToConciseString(IDocOp docOp)
        {
            var sb = new StringBuilder();
            docOp.Apply(CreateConciseStringBuilder(docOp, sb));
            return sb.ToString();
        }

        private static string ToConciseString(IAttributes attributes)
        {
            if (!attributes.Any())
            {
                return "{}";
            }

            var sb = new StringBuilder();
            sb.Append("{");
            bool first = true;
            foreach (var entry in attributes)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append(", ");
                }
                sb.Append(entry.Key);
                sb.Append("=");
                sb.Append(LiteralString(entry.Value));
            }
            sb.Append(" }");
            return sb.ToString();
        }

        private static string ToConciseString(IAnnotationBoundaryMap map)
        {
            var b = new StringBuilder();
            b.Append("{ ");
            bool notEmpty = false;
            for (int i = 0; i < map.EndSize(); ++i)
            {
                if (notEmpty)
                {
                    b.Append(", ");
                }
                else
                {
                    notEmpty = true;
                }
                b.Append(LiteralString(map.GetEndKey(i)));
            }
            for (int i = 0; i < map.ChangeSize(); ++i)
            {
                if (notEmpty)
                {
                    b.Append(", ");
                }
                else
                {
                    notEmpty = true;
                }
                b.Append(LiteralString(map.GetChangeKey(i)));
                b.Append(": ");
                b.Append(LiteralString(map.GetOldValue(i)));
                b.Append(" -> ");
                b.Append(LiteralString(map.GetNewValue(i)));
            }
            b.Append(" }");
            return notEmpty ? b.ToString() : "{}";
        }

        private static string ToConciseString(IAttributesUpdate update)
        {
            if (update.ChangeSize() == 0)
            {
                return "{}";
            }
            var b = new StringBuilder();
            b.Append("{ ");
            for (int i = 0; i < update.ChangeSize(); ++i)
            {
                if (i > 0)
                {
                    b.Append(", ");
                }
                b.Append(update.GetChangeKey(i));
                b.Append(": ");
                b.Append(LiteralString(update.GetOldValue(i)));
                b.Append(" -> ");
                b.Append(LiteralString(update.GetNewValue(i)));
            }
            b.Append(" }");
            return b.ToString();
        }

        private static IDocOpCursor CreateConciseStringBuilder(IDocOp docOp, StringBuilder sb)
        {
            return new SimpleDocOpCursor(docOp, sb);
        }

        private static string EscapeLiteral(string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        private static string LiteralString(string str)
        {
            return string.IsNullOrEmpty(str)
                ? "null"
                : "\"" + EscapeLiteral(str) + "\"";
        }

        public static IDocInitialization AsInitialization(IBufferedDocOp bufferedDocOp)
        {
            if (bufferedDocOp is IDocInitialization)
            {
                return (IDocInitialization) bufferedDocOp;
            }
            return new BufferedDocInitialization(bufferedDocOp);
        }

        private class SimpleDocOpCursor
            : IDocOpCursor
        {
            private readonly IDocOp _docOp;
            private readonly StringBuilder _sb;

            public SimpleDocOpCursor(IDocOp docOp, StringBuilder sb)
            {
                _docOp = docOp;
                _sb = sb;
            }

            public void AnnotationBoundary(IAnnotationBoundaryMap map)
            {
                _sb.Append("|| " + ToConciseString(map) + "; ");
            }

            public void Characters(string characters)
            {
                _sb.Append("++" + LiteralString(characters) + "; ");
            }

            public void ElementStart(string type, IAttributes attributes)
            {
                _sb.Append("<< " + type + " " + ToConciseString(attributes) + "; ");
            }

            public void ElementEnd()
            {
                _sb.Append(">>; ");
            }

            public void Retain(int itemCount)
            {
                _sb.Append("__" + itemCount + "; ");
            }

            public void DeleteCharacters(string characters)
            {
                _sb.Append("--" + LiteralString(characters) + "; ");
            }

            public void DeleteElementStart(string type, IAttributes attributes)
            {
                _sb.Append("x< " + type + " " + ToConciseString(attributes) + "; ");
            }

            public void DeleteElementEnd()
            {
                _sb.Append("x>; ");
            }

            public void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                _sb.Append("r@ " + ToConciseString(oldAttributes) + " " + ToConciseString(newAttributes) + "; ");
            }

            public void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                _sb.Append("u@ " + ToConciseString(attributesUpdate) + "; ");
            }
        }
    }
}