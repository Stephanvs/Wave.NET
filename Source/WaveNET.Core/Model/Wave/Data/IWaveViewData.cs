using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Id;

namespace WaveNET.Core.Model.Wave.Data
{
	public interface IWaveViewData
	{
		/// <summary>
		/// Gets the unique identifier of the wave in view.
		/// </summary>
		/// <returns></returns>
		WaveId GetWaveId();

		/// <summary>
		/// Gets the wavelets in this wave view. The order of iteration is unspecified.
		/// </summary>
		/// <returns>Wavelets in this wave view.</returns>
		IEnumerable<IWaveletData> GetWavelets();

		/// <summary>
		/// Gets a wavelet from the view by it's id
		/// </summary>
		/// <param name="waveletId">The <see cref="WaveletId"/> of the Wavelet to get.</param>
		/// <returns>The requested wavelet, or null if it's not in view.</returns>
		IWaveletData GetWavelet(WaveletId waveletId);

		/// <summary>
		/// Creates a new wavelet in the wave.
		/// </summary>
		/// <param name="waveletId">The new waveletId, which must be unique in the wave.</param>
		/// <returns>The <see cref="IWaveletData"/> for the created Wavelet.</returns>
		IWaveletData CreateWavelet(WaveletId waveletId);

		/// <summary>
		/// Removes a wavelet in the wave.
		/// </summary>
		/// <param name="waveletId">The id of the wavelet to remove, which must be present in the wave.</param>
		void RemoveWavelet(WaveletId waveletId);
	}
}
