namespace WaveNET.Core.Model.Document.Operation.Automation
{
    /// <summary>
    /// The overall result of validating an operation.
    /// </summary>
    public enum ValidationResult
    {
        // These need to be ordered most severe to least severe due to the way
        // we implement mergeWith().
        IllFormed,

        InvalidDocument,

        InvalidSchema,

        Valid
    }
}