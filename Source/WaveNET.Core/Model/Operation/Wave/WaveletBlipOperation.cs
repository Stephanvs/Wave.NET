using System;
using System.Collections.Generic;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    public sealed class WaveletBlipOperation
        : WaveletOperation
    {
        public WaveletBlipOperation(WaveletOperationContext context)
            : base(context)
        {
        }

        protected override void DoApply(IWaveletData wavelet)
        {
            throw new NotImplementedException();
        }

        public override IList<WaveletOperation> ApplyAndReturnReverse(IWaveletData target)
        {
            throw new NotImplementedException();
        }

        public override void AcceptVisitor(IWaveletOperationVisitor visitor)
        {
            visitor.VisitWaveletBlipOperation(this);
        }
    }
}