using System;
using WaveNET.Core.Model.Document.Operation.Automaton;

namespace WaveNET.Core.Model.Document.Operation.Validation
{
    public static class DocOpValidator
    {
        /// <summary>
        /// Returns whether op is well-formed.
        /// 
        /// Any violations recorded in the output <paramref name="collector"/> that are not well-formedness 
        /// violations are meaningless.
        /// </summary>
        public static bool IsWellformed(ViolationCollector collector, IDocOp docOp)
        {
            if (docOp is IBufferedDocOp)
            {
                return IsWellformed(collector, (BufferedDocOp) docOp);
            }
            return IsWellformedRaw(collector, docOp);
        }

        /// <summary>
        /// Same as <see cref="IsWellformed" />, but with a fast path for already-validated instances of <see cref="BufferedDocOp"/>
        /// </summary>
        private static bool IsWellformed(ViolationCollector collector, BufferedDocOp docOp)
        {
            if (docOp.IsKnownToBeWellformed)
            {
                return true;
            }
            
            if (IsWellformedRaw(collector, docOp))
            {
                docOp.IsKnownToBeWellformed = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Same as <see cref="IsWellformed"/>, but without the fast path for <see cref="BufferedDocOp"/>
        /// </summary>
        private static bool IsWellformedRaw(ViolationCollector collector, IDocOp docOp)
        {
            // We validate the operation against the empty document.  It will likely
            // be invalid; however, we ignore the validity aspect anyway since we
            // only care about well-formedness.
            var validationResult =
                Validate(collector, DocumentSchema.NoSchemaConstraints, DocOpAutomaton.EmptyDocument, docOp);

            return validationResult != ValidationResult.IllFormed;
        }

        private static ValidationResult Validate(ViolationCollector collector, IDocumentSchema schema, IAutomatonDocument document, IDocOp docOp)
        {
            if (schema == null)
            {
                schema = DocumentSchema.NoSchemaConstraints;
            }

            var automation = new DocOpAutomaton(document, schema);
            var accu = new ValidationResult[] { ValidationResult.Valid };
            try
            {
                docOp.Apply(new ValidationDocOpCursor(collector, automation, accu));
            }
            catch (IllFormedException illFormed)
            {
                return ValidationResult.IllFormed;
            }

            accu[0] = accu[0].MergeWith(automation.CheckFinish(collector));
            return accu[0];
        }
    }
}