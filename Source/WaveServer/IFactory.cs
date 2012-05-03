using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Server
{
	public interface IFactory
	{
		IWaveletFederationListener ListenerForDomain(string domain);
	}
}
