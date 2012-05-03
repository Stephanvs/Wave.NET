using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Server
{
	public interface IHistoryResponseListener
	{
		void OnSuccess(List<String> deltaList, long lastCommittedVersion, long versionTruncatedAt);
		void OnFailure(String errorMessage);
	}
}
