using System;

namespace WaveNET.Core.Model.Document.Operation.Automation
{
    /// <summary>
    ///     The query methods that DocOpAutomaton needs.
    /// </summary>
    public interface IAutomationDocument
    {
        /// <summary>
        ///     If pos is at an element start, return that element's tag.  Else return null.
        /// </summary>
        String ElementStartingAt(int position);

        /// <summary>
        ///     If pos is at an element start, return that element's attributes.  Else return null.
        /// </summary>
        IAttributes AttributesAt(int position);

        /// <summary>
        ///     If pos is at an element end, return that element's tag.  Else return null.
        /// </summary>
        String ElementEndingAt(int position);

        /// <summary>
        ///     -1 if no char at that position
        /// </summary>
        int CharAt(int position);

        /// <summary>
        ///     depth = 0 means enclosing element, depth = 1 means its parent,
        ///     etc. Null return value means nesting is not that deep.
        /// </summary>
        String NthEnclosingElementTag(int insertionPoint, int depth);

        /// <summary>
        ///     Number of chars after insertionPoint before the next element start or end or document end.
        /// </summary>
        int RemainingCharactersInElement(int insertionPoint);

        IAnnotationMap AnnotationsAt(int position);
        String GetAnnotations(int position, String key);
        int Length();

        /// <summary>
        ///     Finds the first item within a range with the specified key having a value other than fromValue.
        /// </summary>
        /// <param name="start">Start of the range to search</param>
        /// <param name="end">End of the range to search</param>
        /// <param name="key">Key to search for</param>
        /// <param name="fromValue">Value to check for change from</param>
        /// <returns>The location of the first change, or -1 if there is no change</returns>
        int FirstAnnotationChange(int start, int end, String key, String fromValue);
    }
}