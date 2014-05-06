using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation.Automaton
{
    public class ViolationCollector
    {
        private readonly List<OperationIllFormed> _operationIllFormed = new List<OperationIllFormed>();
        private readonly List<OperationInvalid> _operationInvalid = new List<OperationInvalid>();
        private readonly List<SchemaViolation> _schemaViolations = new List<SchemaViolation>();

        public void Add(OperationIllFormed v)
        {
            _operationIllFormed.Add(v);
        }

        public void Add(OperationInvalid v)
        {
            _operationInvalid.Add(v);
        }

        public void Add(SchemaViolation v)
        {
            _schemaViolations.Add(v);
        }

        /// <summary>
        /// True iff at least one violation of the well-formedness constraints was detected.
        /// </summary>
        public bool IsIllFormed()
        {
            return GetValidationResult() == ValidationResult.IllFormed;
        }

        /// <summary>
        /// True iff the most severe validation constraint detected was <see cref="ValidationResult.InvalidDocument"/>
        /// </summary>
        public bool IsInvalidDocument()
        {
            return GetValidationResult() == ValidationResult.InvalidDocument;
        }

        /// <summary>
        /// True iff the most severe validation constraint detected was <see cref="ValidationResult.InvalidSchema"/>
        /// </summary>
        public bool IsInvalidSchema()
        {
            return GetValidationResult() == ValidationResult.InvalidSchema;
        }

        /// <summary>
        /// True iff there were no violations.
        /// </summary>
        public bool IsValid()
        {
            return GetValidationResult() == ValidationResult.Valid;
        }

        /// <summary>
        /// The merged (most severe) validation result
        /// </summary>
        public ValidationResult GetValidationResult()
        {
            if (_operationIllFormed.Any())
            {
                return ValidationResult.IllFormed;
            }
            if (_operationInvalid.Any())
            {
                return ValidationResult.InvalidDocument;
            }
            if (_schemaViolations.Any())
            {
                return ValidationResult.InvalidSchema;
            }
            return ValidationResult.Valid;
        }

        /// <summary>
        /// Returns a description of a single violation, or null if there are none.
        /// </summary>
        public string FirstDescription()
        {
            foreach (var validation in _operationIllFormed)
            {
                return "ill-formed: " + validation.Description;
            }
            foreach (var validation in _operationInvalid)
            {
                return "invalid operation: " + validation.Description;
            }
            foreach (var validation in _schemaViolations)
            {
                return "schema violation: " + validation.Description;
            }
            return null;
        }

        // Todo: What to do with this method?
        ///// <summary>
        ///// Prints descriptions of violations that have been detected, prefixing each line of output with the given prefix.
        ///// </summary>
        //public void PrintDescriptions(PrintStream out, string prefix = "")
        //{
        //    if (IsValid())
        //    {
        //        out.println(prefix + "no violations");
        //        return;
        //    }
        //    foreach (OperationIllFormed v in _operationIllFormed)
        //    {
        //        out.println(prefix + "ill-formed: " + v.Description);
        //    }
        //    foreach (OperationInvalid v in _operationInvalid)
        //    {
        //        out.println(prefix + "invalid operation: " + v.Description);
        //    }
        //    foreach (SchemaViolation v in _schemaViolations)
        //    {
        //        out.println(prefix + "schema violation: " + v.Description);
        //    }
        //}

        private int Size()
        {
            return _operationIllFormed.Count + _operationInvalid.Count + _schemaViolations.Count;
        }

        public override string ToString()
        {
            if (Size() == 0)
            {
                return "ViolationCollector[0]";
            }
            var b = new StringBuilder();
            b.Append("ViolationCollector[" +
                     Size() + ": " + FirstDescription() + "]");
            return b.ToString();
        }
    }
}