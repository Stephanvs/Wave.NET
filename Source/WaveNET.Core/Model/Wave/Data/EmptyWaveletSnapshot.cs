using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Version;

namespace WaveNET.Core.Model.Wave.Data
{
    /// <summary>
    /// A basic implementation for an empty ReadableWaveletData.
    /// </summary>
    public class EmptyWaveletSnapshot
        : IReadableWaveletData
    {
        /// <summary>
        /// Creates an empty snapshot.
        /// </summary>
        public EmptyWaveletSnapshot(WaveId waveId, WaveletId waveletId, ParticipantId creator, HashedVersion version, DateTime creationTime)
        {
            WaveId = waveId;
            WaveletId = waveletId;
            Creator = creator;
            HashedVersion = version;
            CreationTime = creationTime;
        }

        public ParticipantId Creator { get; private set; }
        
        public long Version { get; private set; }
        
        public DateTime CreationTime { get; private set; }
        
        public DateTime LastModifiedTime { get; private set; }
        
        public HashedVersion HashedVersion { get; private set; }
        
        public WaveId WaveId { get; private set; }
        
        public WaveletId WaveletId { get; private set; }
        
        public IReadableBlipData GetDocument(string documentName)
        {
            return default(IReadableBlipData);
        }

        public ICollection<string> GetDocumentIds()
        {
            return new List<string>(0);
        }

        public ReadOnlyCollection<ParticipantId> GetParticipants()
        {
            return new ReadOnlyCollection<ParticipantId>(new List<ParticipantId>(0));
        }
    }
}