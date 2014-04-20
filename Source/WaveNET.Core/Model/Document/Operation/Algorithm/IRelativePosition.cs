namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    /// <summary>
    /// A tracker that tracks the positions of two cursors relative to each other.
    /// </summary>
    public interface IRelativePosition
    {
        /// <summary>
        /// Increases the relative position of the cursor.
        /// </summary>
        /// <param name="amount">the amount by which to increase the relative position</param>
        void Increase(int amount);

        /// <summary>
        /// Gets the relative position
        /// </summary>
        int Get();
    }
}