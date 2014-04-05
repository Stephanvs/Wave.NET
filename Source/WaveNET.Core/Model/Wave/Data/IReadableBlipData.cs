using System;
using System.Collections.ObjectModel;

namespace WaveNET.Core.Model.Wave.Data
{
    /// <summary>
    /// Defines the readable abstract data type used to implement a blip's state.
    /// </summary>
    public interface IReadableBlipData
    {
        /// <summary>
        /// Gets the wavelet in which this blip appears.
        /// </summary>
        IReadableWaveletData Wavelet { get; }

        /// <summary>
        /// Gets the wave participant that created this blip.
        /// </summary>
        ParticipantId Author { get; }

        /// <summary>
        /// Gets an immutable set of contributors, in order
        /// </summary>
        ReadOnlyCollection<ParticipantId> Contributors { get; } 

        /// <summary>
        /// Gets the time of the last modification to this blip.
        /// </summary>
        DateTime LastModifiedTime { get; }

        /// <summary>
        /// Gets the wavelet version of the last modification to this blip.
        /// </summary>
        long LastModifiedVersion { get; }

        /// <summary>
        /// Gets the document content of this blip.
        /// </summary>
        IDocumentOperationSink Content { get; }

        /// <summary>
        /// Gets an identifier for this blip, which is unique within the wavelet.
        /// </summary>
        string Id { get; }
    }
}