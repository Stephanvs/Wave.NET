using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
	public sealed class NoOp : WaveletOperation
	{
		public NoOp() { }

		protected override void DoApply(WaveletData wavelet)
		{
			// do nothing.
		}

		public override WaveletOperation GetInverse()
		{
			return this;
		}

		public override string ToString()
		{
			return "NoOp()";
		}
	}
}
