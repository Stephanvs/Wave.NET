using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Server
{
	public interface IWaveletUpdateCallback
	{
		void OnSuccess();
		void OnFailure(string errorMessage);
	}
}
