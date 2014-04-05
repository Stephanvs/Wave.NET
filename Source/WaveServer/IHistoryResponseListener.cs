using System;
using System.Collections.Generic;

namespace WaveNET.Server
{
    public interface IHistoryResponseListener
    {
        void OnSuccess(List<String> deltaList, long lastCommittedVersion, long versionTruncatedAt);
        void OnFailure(String errorMessage);
    }
}