using System;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Document.Operation.Automation
{
    /// <summary>
    /// The overall result of validating an operation.
    /// </summary>
    public enum ValidationResult
        : int
    {
        // These need to be ordered most severe to least severe due to the way
        // we implement MergeWith().


        IllFormed = 0,

        InvalidDocument = 1,

        InvalidSchema = 2,

        Valid = 3
    }

    public static class ValidationResultExtensions
    {
        public static ValidationResult MergeWith(this ValidationResult vr, ValidationResult other)
        {
            Preconditions.CheckNotNull(other, "Null ValidationResult");

            var min = Math.Min((int) vr, (int) other);

            var name = Enum.GetNames(typeof (ValidationResult))[min - 1]; // Minus one to convert to index
            
            return (ValidationResult) Enum.Parse(typeof (ValidationResult), name, ignoreCase: false);
        }
    }
}