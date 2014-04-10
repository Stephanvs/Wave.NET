using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WaveNET.Core.Model.Document.Util;
using WaveNET.Core.Model.Wave;
using WaveNET.Core.Model.Wave.Data;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Operation.Wave
{
    public sealed class WaveletBlipOperation
        : WaveletOperation
    {
        public WaveletBlipOperation(string blipId, BlipOperation blipOp)
            : base(blipOp.Context)
        {
            Preconditions.CheckNotNullOrEmpty(blipId);
            Preconditions.CheckNotNull(blipOp);

            BlipId = blipId;
            BlipOp = blipOp;
        }

        public string BlipId { get; set; }

        public BlipOperation BlipOp { get; set; }

        protected override void DoApply(IWaveletData wavelet)
        {
            var blip = GetTargetBlip(wavelet);
            BlipOp.Apply(blip);
        }

        /// <summary>
        /// Gets the blip targeted by this operation, creating a data document if it does not exist.
        /// </summary>
        /// <param name="target"></param>
        /// <returns>blip targeted by this operation, never null</returns>
        private IBlipData GetTargetBlip(IWaveletData target)
        {
            return target.GetDocument(BlipId) 
                ?? target.CreateDocument(BlipId, Context.Creator,
                    new Collection<ParticipantId> { Context.Creator }, 
                    EmptyDocument.Empty,
                    Context.Timestamp, target.Version + Context.VersionIncrement);            
        }

        public override IList<WaveletOperation> ApplyAndReturnReverse(IWaveletData target)
        {
            var blip = GetTargetBlip(target);
            var operations = BlipOp.ApplyAndReturnReverse(blip);
            var ret = new List<WaveletOperation>();
            foreach (var op in operations) 
            {
                ret.Add(new WaveletBlipOperation(BlipId, op));
            }
            Update(target);
            return ret;
        }

        public override void AcceptVisitor(IWaveletOperationVisitor visitor)
        {
            visitor.VisitWaveletBlipOperation(this);
        }
    }
}