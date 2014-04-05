using System;
using System.Collections.Generic;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
	public sealed class NoOp 
		: WaveletOperation
	{
	    private static readonly int Hash = typeof(NoOp).Name.GetHashCode();

		public NoOp() { }

		protected override void DoApply(WaveletData wavelet)
		{
			// do nothing.
		}

		public override WaveletOperation GetInverse()
		{
			return this;
		}

		public override IList<WaveletOperation> ApplyAndReturnReverse(WaveletData target)
		{
			throw new NotImplementedException();
		}

		public override void AcceptVisitor(IWaveletOperationVisitor visitor)
		{
			throw new NotImplementedException();
		}

	    public override int GetHashCode()
	    {
	        return Hash;
	    }

	    public override string ToString()
		{
			return "NoOp " + SuffixForToString();
		}
	}
}
