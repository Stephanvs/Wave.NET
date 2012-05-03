using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Server
{
	public interface IDeltaSignerInfoResponseListener
	{
		void OnSuccess(ProtocolSignerInfo signerInfo);
		void OnFailure(String errorMessage);
	}
}
