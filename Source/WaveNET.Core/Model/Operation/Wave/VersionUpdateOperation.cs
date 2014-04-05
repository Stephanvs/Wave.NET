using System.Collections.Generic;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    public sealed class VersionUpdateOperation
        : WaveletOperation
    {
        protected override void DoApply(WaveletData wavelet)
        {
            throw new System.NotImplementedException();
        }

        public override WaveletOperation GetInverse()
        {
            throw new System.NotImplementedException();
        }

        public override IList<WaveletOperation> ApplyAndReturnReverse(WaveletData target)
        {
            throw new System.NotImplementedException();
        }

        public override void AcceptVisitor(IWaveletOperationVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}