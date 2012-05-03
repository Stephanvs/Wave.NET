using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Id;

namespace WaveNET.Server
{
	public interface IWaveletProvider
	{
		void SetListener(IWaveletListener listener);
		void SubmitRequest(WaveletName waveletName, ProtocolWaveletDelta delta, SubmitResultListener listener);
		IList<ProtocolWaveletDelta> RequestHistory(WaveletName waveletName, ProtocolHashedVersion versionStart, ProtocolHashedVersion versionEnd);
	}
}
