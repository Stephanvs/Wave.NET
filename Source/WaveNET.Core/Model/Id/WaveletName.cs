﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Id
{
	public class WaveletName : IComparable<WaveletName>
	{
		public WaveId _waveId;
		public WaveletId _waveletId;

		/// <summary>
		/// Constructs a wavelet name for a wave id and wavelet id.
		/// </summary>
		/// <param name="waveId"></param>
		/// <param name="waveletId"></param>
		/// <returns></returns>
		public static WaveletName Of(WaveId waveId, WaveletId waveletId) {
			return new WaveletName(waveId, waveletId);
		}

		/// <summary>
		/// Constructs a wavelet name for a serialised wave id and wavelet id.
		/// </summary>
		/// <param name="waveId"></param>
		/// <param name="waveletId"></param>
		/// <returns></returns>
		public static WaveletName Of(String waveId, String waveletId) {
			return new WaveletName(WaveId.Deserialize(waveId), WaveletId.Deserialize(waveletId));
		}

		/// <summary>
		/// Private constructor to allow future instance optimisation.
		/// </summary>
		private WaveletName(WaveId waveId, WaveletId waveletId)
		{
			_waveId = waveId;
			_waveletId = waveletId;
		}

		public override string ToString()
		{
			return _waveId + "/" + _waveletId;
		}

		public override bool Equals(object obj)
		{
			if (obj is WaveletName)
			{
				WaveletName other = (WaveletName)obj;
				return _waveId.Equals(other._waveId) && _waveletId.Equals(other._waveletId);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _waveId.GetHashCode() * 37 + _waveletId.GetHashCode();
		}

		public int CompareTo(WaveletName other)
		{
			return _waveId.Equals(other._waveId) ? _waveletId.CompareTo(other._waveletId)
				: _waveId.CompareTo(other._waveId);
		}
	}
}
