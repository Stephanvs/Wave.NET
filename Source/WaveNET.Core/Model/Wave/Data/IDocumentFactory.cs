using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Id;

namespace WaveNET.Core.Model.Wave.Data
{
    /// <summary>
    ///     Factory interface for creating new documents within a wave view.
    /// </summary>
    public interface IDocumentFactory
    {
        /// <summary>
        ///     Creates a new document with the given content. The document's identity within the wave view is provided such that
        ///     an implementation of this interface may keep track of the documents within a wave view, providing domain-specific
        ///     behavior for them.
        /// </summary>
        /// <param name="waveletId">wavelet in which the new document is being created</param>
        /// <param name="docId">id of the new document</param>
        /// <param name="content">content for the new document</param>
        /// <returns>a new document</returns>
        T Create<T>(WaveletId waveletId, string docId, IDocInitialization content) where T : IDocumentOperationSink;
    }
}