using System;
using WaveNET.Core.Model.Version;
using WaveNET.Core.Model.Wave;

namespace WaveNET.Core.Model.Operation.Wave
{
    /// <summary>
    ///     Encapsulates context information for a wave operation.
    /// </summary>
    public class WaveletOperationContext
    {
        internal WaveletOperationContext()
        {
        }

        /// <summary>
        ///     Creates a context.
        /// </summary>
        /// <param name="creator">operation creator</param>
        /// <param name="timestamp">operation time</param>
        /// <param name="versionIncrement">number of version increment</param>
        /// <param name="hashedVersion">new hashed version (or null)</param>
        public WaveletOperationContext(ParticipantId creator, DateTime timestamp, long versionIncrement,
                                       HashedVersion hashedVersion = null)
        {
            Creator = creator;
            Timestamp = timestamp;
            VersionIncrement = versionIncrement;
            HashedVersion = hashedVersion;
        }

        /// <summary>
        ///     The participant that caused an operation.
        /// </summary>
        public ParticipantId Creator { get; private set; }

        /// <summary>
        ///     Time at which an operation occurred.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        ///     Number of versions to increment after applying this operation.
        /// </summary>
        public long VersionIncrement { get; private set; }

        /// <summary>
        ///     Hashed version of the wavelet after applying this operation (optional).
        /// </summary>
        public HashedVersion HashedVersion { get; private set; }

        public DateTime LastModifiedTime { get; private set; }

        public bool HasHashedVersion
        {
            get { return HashedVersion != null; }
        }

        public bool HasTimestamp
        {
            get { return Timestamp != new DateTime(); }
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