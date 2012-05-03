using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
	public abstract class WaveletOperation : IOperation<WaveletData>
	{
		/// <summary>
		/// This method delegates the operation logic to <see cref="DoApply(WaveletData)"/>
		/// </summary>
		/// <param name="wavelet"></param>
		public void Apply(WaveletData wavelet)
		{
			// Execute subtype logic first, because if the subtype logic throws an exception, we must
			// leave this wrapper untouched as though the operation never happened. The subtype is
			// responsible for making sure if they throw an exception they must leave themselves in a
			// state as if the op never happened.
			DoApply(wavelet);
		}

		/// <summary>
		/// Applies this operation's logic to a given wavelet. This method can be
		/// arbitrarily overridden by subclasses.
		/// </summary>
		/// <param name="wavelet">Wavelet on which this operation is to apply itself</param>
		protected abstract void DoApply(WaveletData wavelet);

		/// <summary>
		/// Get the inverse of the operation, such that any <see cref="WaveletData"/> 
		/// object applying this operation followed by its inverse will remain unchanged.
		/// </summary>
		/// <returns>The inverse of this operation</returns>
		public abstract WaveletOperation GetInverse();
	}
}
