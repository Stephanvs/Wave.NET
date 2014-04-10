using System;

namespace WaveNET.Core.Model.Wave.Data
{
    /// <summary>
    ///     Defines the abstract data type used to implement a blip's state.
    /// </summary>
    public interface IBlipData
        : IReadableBlipData
    {
        /// <summary>
        ///     Gets the wavelet in which this blip appears.
        /// </summary>
        new IWaveletData Wavelet { get; }

        /// <summary>
        ///     Gets or sets the last-modified time of this blip
        /// </summary>
        new DateTime LastModifiedTime { get; set; }

        /// <summary>
        ///     Gets or sets the last-modified version of this blip.
        /// </summary>
        new long LastModifiedVersion { get; set; }

        /// <summary>
        ///     Notifies this blip that it has been submitted.
        /// </summary>
        void Submit();

        /// <summary>
        ///     Adds a contributor to this blip. The new contributor is added to the end of the contributors collection if it is
        ///     not already present.
        /// </summary>
        /// <param name="participant"></param>
        void AddContributor(ParticipantId participant);

        /// <summary>
        ///     Removes a contributor from this blip, ensuring it is no longer reflected in the contributors collection.
        /// </summary>
        /// <param name="participant"></param>
        void RemoveContributor(ParticipantId participant);

        /// <summary>
        ///     Notifies the BlipData that the content of the document inside the blip have changed.
        /// </summary>
        void OnRemoteContentModified();
    }
}