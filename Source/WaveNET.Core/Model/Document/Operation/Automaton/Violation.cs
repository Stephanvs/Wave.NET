namespace WaveNET.Core.Model.Document.Operation.Automaton
{
    /// <summary>
    ///     An object containing information about one individual reason why an
    ///     operation is not valid, e.g. "retain past end" or "deletion inside insertion".
    /// </summary>
    public abstract class Violation
    {
        protected Violation(string description, int originalPos, int resultingPos)
        {
            Description = description;
            OriginalDocumentPos = originalPos;
            ResultingDocumentPos = resultingPos;
        }

        public abstract ValidationResult ValidationResult { get; }

        public string Description { get; set; }

        public int OriginalDocumentPos { get; set; }

        public int ResultingDocumentPos { get; set; }
    }

    /// <summary>
    ///     An object containing information about the way in which an operation is ill-formed.
    /// </summary>
    public sealed class OperationIllFormed : Violation
    {
        public OperationIllFormed(string description, int originalPos, int resultingPos)
            : base(description, originalPos, resultingPos)
        {
        }

        public override ValidationResult ValidationResult
        {
            get { return ValidationResult.IllFormed; }
        }
    }

    /// <summary>
    ///     An object containing information about how an operation is invalid
    ///     for a reason that does not depend on XML schema constraints.
    /// </summary>
    public sealed class OperationInvalid : Violation
    {
        public OperationInvalid(string description, int originalPos, int resultingPos)
            : base(description, originalPos, resultingPos)
        {
        }

        public override ValidationResult ValidationResult
        {
            get { return ValidationResult.InvalidDocument; }
        }
    }

    /// <summary>
    ///     An object containing information about how an operation violates XML
    ///     schema constraints.
    /// </summary>
    public sealed class SchemaViolation : Violation
    {
        public SchemaViolation(string description, int originalPos, int resultingPos)
            : base(description, originalPos, resultingPos)
        {
        }

        public override ValidationResult ValidationResult
        {
            get { return ValidationResult.InvalidSchema; }
        }
    }
}