using System;
using WaveNET.Core.Model.Document.Operation.Automation;

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
                Validate(collector, null, null, docOp);
                //Validate(collector, DocumentSchema.NO_SCHEMA_CONSTRAINTS, DocOpAutomation.EmptyDocument, docOp);

            throw new NotImplementedException();

            return validationResult == ValidationResult.IllFormed;
        }

        private static ValidationResult Validate(ViolationCollector collector, IDocumentSchema schema, IAutomationDocument document, IDocOp docOp)
        {
            return ValidationResult.IllFormed;
        }
    }
}