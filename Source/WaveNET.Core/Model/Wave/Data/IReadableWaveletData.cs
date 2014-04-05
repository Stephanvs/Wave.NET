using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Version;

namespace WaveNET.Core.Model.Wave.Data
{
    public interface IReadableWaveletData
    {
        /// <summary>
        /// Gets this wavelet's document identified by the provided name.
        /// </summary>
        /// <param name="documentName">Name of the document</param>
        /// <returns>the requested document.</returns>
        IReadableBlipData GetDocument(string documentName);

        /// <summary>
        /// Gets a set of the ids of all non-empty documents in this wavelet.
        /// </summary>
        /// <returns></returns>
        ICollection<string> GetDocumentIds();

        /// <summary>
        /// Get the participant that created this wavelet. The creator is immutable.
        /// </summary>
        ParticipantId Creator { get; }

        /// <summary>
        /// An immutable list of this wave's participants, in the order in which they were added.
        /// </summary>
        /// <returns>the wavelet's participants.</returns>
        ReadOnlyCollection<ParticipantId> GetParticipants();

        /// <summary>
        /// Gets the latest version number of this wavelet known to exist at the server.
        /// </summary>
        long Version { get; } 

        /// <summary>
        /// Gets the date and time at which the wavelet was created.
        /// </summary>
        DateTime CreationTime { get; }

        /// <summary>
        /// Gets the time of the last modification made to this wavelet (including any of its documents).
        /// </summary>
        DateTime LastModifiedTime { get; }

        /// <summary>
        /// Gets the latest distinct version number of this wavelet known to exist at the server.
        /// </summary>
        /// <remarks>The version number may be earlier than <see cref="Version"/></remarks>
        HashedVersion HashedVersion { get; }

        /// <summary>
        /// Gets the id of the wave containing this wavelet.
        /// </summary>
        WaveId WaveId { get; }

        /// <summary>
        /// Gets the identifier of this wavelet. Wavelet ids are unique within a wave.
        /// </summary>
        WaveletId WaveletId { get; }
    }
}