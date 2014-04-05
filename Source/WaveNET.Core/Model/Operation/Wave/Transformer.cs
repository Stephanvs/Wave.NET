using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Wave;

namespace WaveNET.Core.Model.Operation.Wave
{
	/// <summary>
	/// The class for transforming operations as in the Jupiter system.
	/// 
	/// The Jupiter algorithm takes 2 operations S and C and produces S' and C'.
	/// Where the operations, S + C' = C + S'
	/// </summary>
	public static class Transformer
	{
		/// <summary>
		/// Transforms a pair of operations.
		/// </summary>
		/// <param name="clientOperation">The client's operation.</param>
		/// <param name="serverOperation">The server's operation.</param>
		/// <returns>The resulting transformed client and server operations.</returns>
		public static OperationPair<WaveletOperation> Transform(WaveletOperation clientOperation, WaveletOperation serverOperation)
		{
			if (clientOperation is WaveletDocumentOperation && serverOperation is WaveletDocumentOperation)
			{
				WaveletDocumentOperation clientWaveDocOp = (WaveletDocumentOperation)clientOperation;
				WaveletDocumentOperation serverWaveDocOp = (WaveletDocumentOperation)serverOperation;
				if (clientWaveDocOp.DocumentId.Equals(serverWaveDocOp.DocumentId))
				{
					// Transform document operations
					IBufferedDocOp clientMutation = clientWaveDocOp.Operation;
					IBufferedDocOp serverMutation = serverWaveDocOp.Operation;
					OperationPair<BufferedDocOp> transformedDocOps = Transformer.Transform(clientMutation, serverMutation);
					
					// Only recreate boxes if transform did something.  Yes, this is != not !.equals
					if (transformedDocOps.ClientOperation != clientMutation)
					{
						clientOperation = transformedDocOps.ClientOperation != null
							? new WaveletDocumentOperation(clientWaveDocOp.DocumentId, transformedDocOps.ClientOperation)
							: null;
					}
					if (transformedDocOps.ServerOperation != serverMutation)
					{
						serverOperation = transformedDocOps.ServerOperation != null
							? new WaveletDocumentOperation(serverWaveDocOp.DocumentId, transformedDocOps.ServerOperation) 
							: null;
					}
				}
				else
				{
					// Different documents don't conflict; use identity transform below
				}
			}
			else
			{
				if (serverOperation is RemoveParticipant)
				{
					if (clientOperation is RemoveParticipant)
					{
						ParticipantId clientParticipant = ((RemoveParticipant)clientOperation).Participant;
						ParticipantId serverParticipant = ((RemoveParticipant)serverOperation).Participant;
						if (clientParticipant.Equals(serverParticipant))
						{
							clientOperation = null;
							serverOperation = null;
						}
					}
					else if (clientOperation is AddParticipant)
					{
						CheckParticipantRemovalAndAddition((RemoveParticipant)serverOperation,
							(AddParticipant)clientOperation);
					}
				}
				else if (serverOperation is AddParticipant)
				{
					if (clientOperation is AddParticipant)
					{
						ParticipantId clientParticipant = ((AddParticipant)clientOperation).Participant;
						ParticipantId serverParticipant = ((AddParticipant)serverOperation).Participant;
						if (clientParticipant.Equals(serverParticipant))
						{
							clientOperation = null;
							serverOperation = null;
						}
					}
					else if (clientOperation is RemoveParticipant)
					{
						CheckParticipantRemovalAndAddition((RemoveParticipant)clientOperation, (AddParticipant)serverOperation);
					}
				}
			}
			// Apply identity transform by default
			return new OperationPair<WaveletOperation>(clientOperation, serverOperation);
		}

		/// <summary>
		/// Checks to see if a participant is being removed by one operation and added
		/// by another concurrent operation. In such a situation, at least one of the
		/// operations is invalid.
		/// </summary>
		/// <param name="removeParticipant">The operation to remove a participant.</param>
		/// <param name="addParticipant">The operation to add a participant.</param>
		/// <exception cref="TransformException">TransformException if the same participant is being concurrently added and removed.</exception>
		private static void CheckParticipantRemovalAndAddition(RemoveParticipant removeParticipant, AddParticipant addParticipant)
		{
			if (removeParticipant.Participant.Equals(addParticipant.Participant))
				throw new TransformException("Transform error involving participant: " + removeParticipant.Participant.GetAddress());
		}
	}
}
