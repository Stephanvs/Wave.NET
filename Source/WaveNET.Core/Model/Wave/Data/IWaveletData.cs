using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Version;

namespace WaveNET.Core.Model.Wave.Data
{
    public interface IWaveletData<out TData>
        : IWaveletData
    {
        new TData GetDocument(string documentName);

        new TData CreateDocument(string docId, ParticipantId author, ICollection<ParticipantId> contributors,
            IDocInitialization content, DateTime lastModifiedTime, long lastModifiedVersion);
    }

    public interface IWaveletData
        : IReadableWaveletData
    {
        /// <summary>
        ///     Gets or sets the version number of this wavelet.
        /// </summary>
        new long Version { get; set; }

        /// <summary>
        ///     Gets or sets the distinct version of this wavelet.
        /// </summary>
        new HashedVersion HashedVersion { get; set; }

        /// <summary>
        ///     Gets or sets the last-modified time of this wavelet.
        /// </summary>
        new DateTime LastModifiedTime { get; set; }

        /// <summary>
        ///     Gets this wavelet's document identified by the provided name.
        /// </summary>
        /// <param name="documentName">Name of the document</param>
        /// <returns>the requested document.</returns>
        new IBlipData GetDocument(string documentName);

        /// <summary>
        ///     Creates a document in this wavelet.
        /// </summary>
        /// <param name="docId">identifier of the document</param>
        /// <param name="author">author of the new document</param>
        /// <param name="contributors">participants who have contributed to the document</param>
        /// <param name="content">initial content of the document</param>
        /// <param name="lastModifiedTime">time of last worthy modification</param>
        /// <param name="lastModifiedVersion">version of last worthy modification</param>
        /// <returns></returns>
        IBlipData CreateDocument(string docId, ParticipantId author, ICollection<ParticipantId> contributors,
            IDocInitialization content, DateTime lastModifiedTime, long lastModifiedVersion);

        /// <summary>
        ///     Adds a participant to this wavelet, ensuring it is in the participants
        ///     collection. The new participant is added to the end of the collection if it
        ///     was not already present.
        /// </summary>
        /// <param name="participant"></param>
        /// <returns>false if the given participant was not a participant of this wavelet.</returns>
        bool AddParticipant(ParticipantId participant);

        /// <summary>
        ///     Adds a participant to this wavelet, ensuring it is in the participants
        ///     collection. The new participant is added to the end of the collection if it
        ///     was not already present.
        /// </summary>
        /// <param name="participant"></param>
        /// <param name="position"></param>
        /// <returns>
        ///     false if the given participant was already a participant of this wavelet, in which case its position is
        ///     unaffected.
        /// </returns>
        bool AddParticipant(ParticipantId participant, int position);

        /// <summary>
        ///     Removes a participant from this wavelet, ensuring it is no longer reflected in the participants collection.
        /// </summary>
        /// <param name="participant">participant to remove</param>
        /// <returns></returns>
        bool RemoveParticipant(ParticipantId participant);
    }
}