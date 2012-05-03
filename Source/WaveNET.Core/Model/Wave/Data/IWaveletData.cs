using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Document.Operation;

namespace WaveNET.Core.Model.Wave.Data
{
	public interface IWaveletData
	{
		/// <summary>
		/// Constructs an unmodifiable mpa view mapping the ids of all non-empty
		/// documents in this wavelet to the corresponding document
		/// </summary>
		Dictionary<String, IBufferedDocOp> GetDocuments();

		/// <summary>
		/// An immutable list of participants, in the order in which they were added.
		/// </summary>
		/// <returns>The wave's participants</returns>
		IList<ParticipantId> GetParticipants();

		/// <summary>
		/// Gets the <see cref="WaveletName"/> that uniquely identifies this Wavelet.
		/// </summary>
		WaveletName WaveletName { get; }

		/// <summary>
		/// Adds a participant to this wave, ensuring it is in the participants collection.
		/// The new participant is added to the end of the collection if it was not already present.
		/// </summary>
		/// <param name="participant">The <see cref="ParticipantId"/> of the participant to add</param>
		/// <returns>False if the participant was already a participant of this wavelet.</returns>
		bool AddParticipant(ParticipantId participant);

		/// <summary>
		/// Removes a participant from this wave, ensuring it is no longer reflected in the participants collection.
		/// </summary>
		/// <param name="participant">The <see cref="ParticipantId"/> of the participant to remove.</param>
		/// <returns>False if the given participant was not a participant of this wavelet.</returns>
		bool RemoveParticipant(ParticipantId participant);

		/// <summary>
		/// Modify the document contained in the wavelet. 
		/// If a document with the given id did not previously exist, it will be created.
		/// </summary>
		bool ModifyDocument(String documentId, IBufferedDocOp op);
	}
}
