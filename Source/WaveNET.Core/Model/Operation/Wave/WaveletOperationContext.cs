using System;
using WaveNET.Core.Model.Version;
using WaveNET.Core.Model.Wave;

namespace WaveNET.Core.Model.Operation.Wave
{
    public sealed class WaveletOperationContext
    {
        public ParticipantId Creator { get; private set; }
        
        public DateTime Timestamp { get; private set; }
        
        public long VersionIncrement { get; private set; }
        
        public HashedVersion HashedVersion { get; private set; }

        public WaveletOperationContext(ParticipantId creator, DateTime timestamp, long versionIncrement)
            : this(creator, timestamp, versionIncrement, null)
        {
        }

        public WaveletOperationContext(ParticipantId creator, DateTime timestamp, long versionIncrement,
            HashedVersion hashedVersion)
        {
            Creator = creator;
            Timestamp = timestamp;
            VersionIncrement = versionIncrement;
            HashedVersion = hashedVersion;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "WaveletOperationContext(creator: {0}, timestamp: {1}, version increment: {2}, hashedVersion: {3})",
                    Creator, Timestamp, VersionIncrement, HashedVersion);
        }
    }
}