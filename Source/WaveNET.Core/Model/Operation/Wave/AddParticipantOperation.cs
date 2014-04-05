using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using WaveNET.Core.Model.Wave;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    public sealed class AddParticipantOperation
        : WaveletOperation
    {
        private const int EndPosition = -1;
        private readonly int _position;

        public AddParticipantOperation(WaveletOperationContext context, ParticipantId participant,
                                       int position = EndPosition)
            : base(context)
        {
            Contract.Requires(participant != null);

            ParticipantId = participant;
            _position = position;
        }

        /// <summary>
        ///     Gets the participant to add.
        /// </summary>
        public ParticipantId ParticipantId { get; private set; }

        protected override void DoApply(IWaveletData wavelet)
        {
            if (!wavelet.AddParticipant(ParticipantId))
                throw new OperationException("Attempt to add a duplicate participant");
        }

        public override IList<WaveletOperation> ApplyAndReturnReverse(IWaveletData target)
        {
            WaveletOperationContext reverseContext = CreateReverseContext(target);
            DoApply(target);
            Update(target);

            return new ReadOnlyCollection<WaveletOperation>(new WaveletOperation[]
            {
                new RemoveParticipantOperation(reverseContext, ParticipantId)
            });
        }

        public override void AcceptVisitor(IWaveletOperationVisitor visitor)
        {
            visitor.VisitAddParticipantOperation(this);
        }

        public override int GetHashCode()
        {
            return ParticipantId.GetHashCode();
        }

        public override string ToString()
        {
            return "AddParticipant(" + ParticipantId + ")";
        }
    }
}