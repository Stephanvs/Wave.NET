using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Document.Operation;
using System.Diagnostics.Contracts;

namespace WaveNET.Core.Model.Operation.Wave
{
	public sealed class WaveletDocumentOperation : WaveletOperation
	{
		/// <summary>
		/// Identifier of the document within the target wavelet to modify
		/// </summary>
		private readonly String _documentId;

		/// <summary>
		/// Document operation which modifies the target document
		/// </summary>
		private readonly IBufferedDocOp _operation;


		public WaveletDocumentOperation(String documentId, IBufferedDocOp operation)
		{
			Contract.Requires(!String.IsNullOrEmpty(documentId));
			Contract.Requires(operation != null);

			_documentId = documentId;
			_operation = operation;
		}

		public String DocumentId { get { return _documentId; } private set { } }
		public IBufferedDocOp Operation { get { return _operation; } private set { } }

		protected override void DoApply(Model.Wave.Data.WaveletData wavelet)
		{
			wavelet.ModifyDocument(_documentId, _operation);
		}

		public override WaveletOperation GetInverse()
		{
			DocOpInverter<IBufferedDocOp> inverse = new DocOpInverter<IBufferedDocOp>(new DocOpBuffer());
			_operation.Apply(inverse);
			return new WaveletDocumentOperation(_documentId, inverse.Finish());
		}

		public override string ToString()
		{
			return String.Format("WaveletDocumentOperation({0},{1})", _documentId, _operation);
		}
	}
}
