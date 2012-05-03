using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Wave;
using System.Diagnostics.Contracts;

namespace WaveNET.Core.Model.Operation.Wave
{
	public sealed class AddParticipant : WaveletOperation
	{
		private readonly ParticipantId _participant;

		public AddParticipant(ParticipantId participant)
		{
			Contract.Ensures(participant != null);

			_participant = participant;
		}

		/// <summary>
		/// Gets the participant to add.
		/// </summary>
		public ParticipantId Participant
		{
			get { return _participant; }
			private set { }
		}

		protected override void DoApply(Model.Wave.Data.WaveletData wavelet)
		{
			if (!wavelet.AddParticipant(_participant)) 
				throw new OperationException("Attempt to add a duplicate participant");
		}

		public override WaveletOperation GetInverse()
		{
			return new RemoveParticipant(_participant);
		}

		public override string ToString()
		{
			return "AddParticipant(" + _participant + ")";
		}
	}
}
