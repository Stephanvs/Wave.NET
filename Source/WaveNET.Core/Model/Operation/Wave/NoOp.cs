using System.Collections.Generic;
using System.Collections.ObjectModel;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    public sealed class NoOp
        : WaveletOperation
    {
        private static readonly int Hash = typeof (NoOp).Name.GetHashCode();

        public NoOp(WaveletOperationContext context)
            : base(context)
        {
        }

        protected override void DoApply(IWaveletData wavelet)
        {
            // do nothing.
        }

        public override IList<WaveletOperation> ApplyAndReturnReverse(IWaveletData target)
        {
            WaveletOperation reverse = new NoOp(CreateReverseContext(target));
            Apply(target);
            return new ReadOnlyCollection<WaveletOperation>(new[] {reverse});
        }

        public override void AcceptVisitor(IWaveletOperationVisitor visitor)
        {
            visitor.VisitNoOp(this);
        }

        public override bool Equals(object obj)
        {
            return obj is NoOp;
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public override string ToString()
        {
            return "no-op " + SuffixForToString();
        }
    }
}