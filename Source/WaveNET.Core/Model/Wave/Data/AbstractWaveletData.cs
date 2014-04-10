using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Version;

namespace WaveNET.Core.Model.Wave.Data
{
    public abstract class AbstractWaveletData<T>
        : IWaveletData
        where T : IBlipData
    {
        protected AbstractWaveletData(WaveletId waveletId, ParticipantId creator, DateTime creationTime, long version,
            HashedVersion hashedVersion, DateTime lastModifiedTime, WaveId waveId)
        {
            WaveletId = waveletId;
            Creator = creator;
            CreationTime = creationTime;
            Version = version;
            HashedVersion = hashedVersion;
            LastModifiedTime = lastModifiedTime;
            WaveId = waveId;
        }

        protected AbstractWaveletData(IReadableWaveletData data)
            : this(
                data.WaveletId, data.Creator, data.CreationTime, data.Version, data.HashedVersion, data.LastModifiedTime,
                data.WaveId)
        {
        }

        public WaveletId WaveletId { get; protected set; }

        public abstract IBlipData GetDocument(string documentName);

        IReadableBlipData IReadableWaveletData.GetDocument(string documentName)
        {
            return GetDocument(documentName);
        }

        public abstract ICollection<string> GetDocumentIds();

        public ReadOnlyCollection<ParticipantId> GetParticipants()
        {
            return new ReadOnlyCollection<ParticipantId>(GetMutableParticipants().ToList());
        }

        public ParticipantId Creator { get; protected set; }

        public DateTime CreationTime { get; protected set; }

        public IBlipData CreateDocument(string id, ParticipantId author, ICollection<ParticipantId> contributors,
            IDocInitialization content,
            DateTime lastModifiedTime, long lastModifiedVersion)
        {
            throw new NotImplementedException();
        }

        public bool AddParticipant(ParticipantId participant)
        {
            IList<ParticipantId> participants = GetMutableParticipants();
            if (participants.Contains(participant))
            {
                return false;
            }

            participants.Add(participant);
            // todo: notify listeners or use ObservableCollection?

            return true;
        }

        public bool AddParticipant(ParticipantId participant, int position)
        {
            IList<ParticipantId> participants = GetMutableParticipants();
            if (participants.Contains(participant))
            {
                return false;
            }
            participants.Insert(position, participant);
            // todo: notify listeners or use ObservableCollection?

            return true;
        }

        public bool RemoveParticipant(ParticipantId participant)
        {
            IList<ParticipantId> participants = GetMutableParticipants();
            if (!participants.Remove(participant))
            {
                return false;
            }

            // todo: notify listeners or use ObservableCollection?

            return true;
        }

        public long Version { get; set; }

        public HashedVersion HashedVersion { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public WaveId WaveId { get; protected set; }

        protected abstract IList<ParticipantId> GetMutableParticipants();
    }
}