using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using WaveNET.Core.Model.Id;

namespace WaveNET.Core.Model.Wave.Data
{
    /// <summary>
    ///     A skeleton implementation of <see cref="IWaveViewData" />
    /// </summary>
    public class WaveViewData : IWaveViewData
    {
        private readonly WaveId _waveId;
        private readonly Dictionary<WaveletId, IWaveletData> _wavelets;

        /// <summary>
        ///     Creates a new instance of the <see cref="WaveViewData" /> class.
        /// </summary>
        /// <param name="waveId">The wave id.</param>
        public WaveViewData(WaveId waveId)
        {
            _waveId = waveId;
            _wavelets = new Dictionary<WaveletId, IWaveletData>();
        }

        public WaveId GetWaveId()
        {
            return _waveId;
        }

        public IEnumerable<IWaveletData> GetWavelets()
        {
            return new ReadOnlyCollection<IWaveletData>(_wavelets.Values.ToList());
        }

        public IWaveletData GetWavelet(WaveletId waveletId)
        {
            return _wavelets[waveletId];
        }

        public IWaveletData CreateWavelet(WaveletId waveletId)
        {
            Contract.Requires(!_wavelets.ContainsKey(waveletId), "Duplicate wavelet id: " + waveletId);

            throw new NotImplementedException();
            //IWaveletData wavelet = new WaveletData(_waveId, waveletId);
            //_wavelets.Add(waveletId, wavelet);

            //return wavelet;
        }

        public void RemoveWavelet(WaveletId waveletId)
        {
            Contract.Ensures(_wavelets.Remove(waveletId), waveletId + " is not present");
        }
    }
}