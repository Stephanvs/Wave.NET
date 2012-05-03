using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Collections.ObjectModel;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Document.Operation;

namespace WaveNET.Core.Model.Wave.Data
{
	/// <summary>
	/// Defines the abstract data type used to describe the content of a wavelet.
	/// </summary>
	public class WaveletData : IWaveletData
	{
		/// <summary>
		/// A document (operation) with no contents.
		/// </summary>
		private static IBufferedDocOp Empty = new DocOpBuffer().Finish();

		private WaveId _waveId;
		private WaveletId _waveletId;
		private List<ParticipantId> _participants;
		private Dictionary<String, IBufferedDocOp> _documents;

		public WaveletData(WaveId waveId, WaveletId waveletId)
		{
			Contract.Requires(waveId != null, "waveId cannot be null");
			Contract.Requires(waveletId != null, "waveletId cannot be null");

			_waveId = waveId; 
			_waveletId = waveletId;
			_participants = new List<ParticipantId>();
			_documents = new Dictionary<string, IBufferedDocOp>();
		}

		private IBufferedDocOp GetOrCreateDocument(String documentId)
		{
			IBufferedDocOp doc = _documents[documentId];
			if (doc == null)
			{
				doc = WaveletData.Empty;
				_documents.Add(documentId, doc);
			}
			return doc;
		}

		public Dictionary<string, IBufferedDocOp> GetDocuments()
		{
			return _documents;
		}

		public IList<ParticipantId> GetParticipants()
		{
			return new ReadOnlyCollection<ParticipantId>(_participants);
		}

		public WaveletName WaveletName
		{
			get { return WaveletName.Of(_waveId, _waveletId); }
		}

		public bool AddParticipant(ParticipantId participant)
		{
			if (_participants.Contains(participant)) 
				return false;
			
			_participants.Add(participant);
			return true;
		}

		public bool RemoveParticipant(ParticipantId participant)
		{
			return _participants.Remove(participant);
		}

		public bool ModifyDocument(string documentId, IBufferedDocOp operation)
		{
			IBufferedDocOp doc = Composer.Compose(GetOrCreateDocument(documentId), operation);
			if (OpComparators.SyntacticIdentity.Equal(doc, WaveletData.Empty))
				_documents.Remove(documentId);
			else
				_documents.Add(documentId, doc);

			return true;
		}


		public override string ToString()
		{
			return String.Format("Wavelet state = {0}, {1} - {2}", _waveId, _waveletId, _documents);
		}
	}
}
