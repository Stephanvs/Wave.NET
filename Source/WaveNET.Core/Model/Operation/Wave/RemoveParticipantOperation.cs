using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Wave;
using System.Diagnostics.Contracts;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
	public sealed class RemoveParticipantOperation 
		: WaveletOperation
	{
		private readonly ParticipantId _participant;

		public RemoveParticipantOperation(ParticipantId participant)
		{
			Contract.Ensures(participant != null);

			_participant = participant;
		}
		
		/// <summary>
		/// Gets the participant to remove.
		/// </summary>
		public ParticipantId Participant
		{
			get { return _participant; }
			private set { }
		}

		protected override void DoApply(Model.Wave.Data.WaveletData wavelet)
		{
			if (wavelet.RemoveParticipant(_participant))
				throw new OperationException("Attempt to delete non-existent participant");
		}

		public override WaveletOperation GetInverse()
		{
			return new AddParticipantOperation(_participant);
		}

	    public override IList<WaveletOperation> ApplyAndReturnReverse(WaveletData target)
	    {
	        throw new NotImplementedException();
	    }

	    public override void AcceptVisitor(IWaveletOperationVisitor visitor)
	    {
	        throw new NotImplementedException();
	    }

	    public override string ToString()
		{
			return "RemoveParticipant(" + _participant + ")";
		}
	}
}
