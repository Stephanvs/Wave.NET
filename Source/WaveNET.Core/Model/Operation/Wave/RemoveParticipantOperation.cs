using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using WaveNET.Core.Model.Wave;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    public sealed class RemoveParticipantOperation
        : WaveletOperation
    {
        public RemoveParticipantOperation(WaveletOperationContext context, ParticipantId participant)
            : base(context)
        {
            Contract.Ensures(participant != null);

            ParticipantId = participant;
        }

        /// <summary>
        ///     Gets the participant to remove.
        /// </summary>
        public ParticipantId ParticipantId { get; private set; }

        protected override void DoApply(IWaveletData wavelet)
        {
            if (!wavelet.RemoveParticipant(ParticipantId))
                throw new OperationException(string.Format("Attempt to delete non-existent participant: {0}",
                    ParticipantId));
        }

        public override IEnumerable<WaveletOperation> ApplyAndReturnReverse(IWaveletData target)
        {
            WaveletOperationContext reverseContext = CreateReverseContext(target);
            int position = DetermineParticipantPosition(target);
            DoApply(target);
            Update(target);

            return new ReadOnlyCollection<WaveletOperation>(new WaveletOperation[]
            {
                new AddParticipantOperation(reverseContext, ParticipantId, position)
            });
        }

        private int DetermineParticipantPosition(IWaveletData target)
        {
            int position = 0;
            foreach (ParticipantId participantId in target.GetParticipants())
            {
                if (participantId.Equals(ParticipantId))
                {
                    return position;
                }
                position++;
            }
            throw new OperationException(string.Format("Attempt to remove non-existent participant: {0}", ParticipantId));
        }

        public override void AcceptVisitor(IWaveletOperationVisitor visitor)
        {
            visitor.VisitRemoveParticipantOperation(this);
        }

        public override int GetHashCode()
        {
            return ParticipantId.GetHashCode();
        }

        public override string ToString()
        {
            return "RemoveParticipant(" + ParticipantId + ")";
        }
    }
}