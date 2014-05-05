namespace WaveNET.Core.Model.Document.Operation.Automation
{
    public class DocOpAutomation
    {
        public static readonly IAutomationDocument EmptyDocument = new EmptyDocumentAutomation();
        private readonly IDocumentSchema _constraints;
        private readonly IAutomationDocument _document;
        private int _effectivePos = 0;
        private int resultingPos = 0;

        /// <summary>
        ///     Creates an automaton that corresponds to the set of all possible operations
        ///     on the given document under the given schema constraints.
        /// </summary>
        public DocOpAutomation(IAutomationDocument document, IDocumentSchema constraints)
        {
            _document = document;
            _constraints = constraints;
        }

        public ValidationResult CheckCharacters(string characters, ViolationCollector collector)
        {
            if (characters == null) return NullCharacters(collector);
            if (string.IsNullOrEmpty(characters)) return EmptyCharacters(collector);

            // Todo: implement other checks

            return Valid();
        }


        private ValidationResult AddViolation(ViolationCollector a, OperationIllFormed v)
        {
            if (a != null)
            {
                a.Add(v);
            }
            return v.ValidationResult;
        }

        private ValidationResult AddViolation(ViolationCollector a, OperationInvalid v)
        {
            if (a != null)
            {
                a.Add(v);
            }
            return v.ValidationResult;
        }

        private ValidationResult AddViolation(ViolationCollector a, SchemaViolation v)
        {
            if (a != null)
            {
                a.Add(v);
            }
            return v.ValidationResult;
        }

        private OperationIllFormed IllFormedOperation(string description)
        {
            return new OperationIllFormed(description, _effectivePos, resultingPos);
        }

        private OperationInvalid InvalidOperation(string description)
        {
            return new OperationInvalid(description, _effectivePos, resultingPos);
        }

        private SchemaViolation SchemaViolation(string description)
        {
            return new SchemaViolation(description, _effectivePos, resultingPos);
        }

        private ValidationResult Valid()
        {
            return ValidationResult.Valid;
        }

        private ValidationResult NullCharacters(ViolationCollector v)
        {
            return AddViolation(v, IllFormedOperation("characters is null"));
        }

        private ValidationResult EmptyCharacters(ViolationCollector v)
        {
            return AddViolation(v, IllFormedOperation("characters is empty"));
        }
    }
}