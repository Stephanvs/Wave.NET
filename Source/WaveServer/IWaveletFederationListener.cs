using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Id;

namespace WaveNET.Server
{
	public interface IWaveletFederationListener
	{
		void WaveletDeltaUpdate(WaveletName waveletName, List<string> deltas, IWaveletUpdateCallback callback);
		void WaveletCommitUpdate(WaveletName waveletName, ProtocolHashedVersion committedVersion, IWaveletUpdateCallback callback);
	}
}
