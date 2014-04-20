using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Version;

namespace WaveNET.Core.Model.Wave.Data
{
    public abstract class AbstractWaveletData<TData>
        : IWaveletData<TData>
        where TData : IBlipData
    {
        private readonly IDocumentFactory _contentFactory;

        protected AbstractWaveletData(WaveletId waveletId, ParticipantId creator, DateTime creationTime, long version,
                                      HashedVersion hashedVersion, DateTime lastModifiedTime, WaveId waveId,
                                      IDocumentFactory contentFactory)
        {
            WaveletId = waveletId;
            Creator = creator;
            CreationTime = creationTime;
            Version = version;
            HashedVersion = hashedVersion;
            LastModifiedTime = lastModifiedTime;
            WaveId = waveId;
            _contentFactory = contentFactory;
        }

        protected AbstractWaveletData(IReadableWaveletData data, IDocumentFactory contentFactory)
            : this(
                data.WaveletId, data.Creator, data.CreationTime, data.Version, data.HashedVersion, data.LastModifiedTime,
                data.WaveId, contentFactory)
        {
        }

        public WaveletId WaveletId { get; protected set; }

        public abstract TData GetDocument(string documentName);

        IBlipData IWaveletData.CreateDocument(string docId, ParticipantId author,
                                              IList<ParticipantId> contributors, IDocInitialization content,
                                              DateTime lastModifiedTime, long lastModifiedVersion)
        {
            return CreateDocument(docId, author, contributors, content, lastModifiedTime, lastModifiedVersion);
        }

        IReadableBlipData IReadableWaveletData.GetDocument(string documentName)
        {
            return GetDocument(documentName);
        }

        IBlipData IWaveletData.GetDocument(string documentName)
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

        public TData CreateDocument(string docId, ParticipantId author, IList<ParticipantId> contributors,
                                    IDocInitialization content,
                                    DateTime lastModifiedTime, long lastModifiedVersion)
        {
            var sink = _contentFactory.Create<IDocumentOperationSink>(WaveletId, docId, content);
            TData doc = InternalCreateDocument(docId, author, contributors, sink, lastModifiedTime, lastModifiedVersion);

            // todo: notify listeners or use ObservableCollection?

            return doc;
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

        protected abstract TData InternalCreateDocument(string docId, ParticipantId author,
                                                        IList<ParticipantId> contributors,
                                                        IDocumentOperationSink contentSink, DateTime lastModifiedTime,
                                                        long lastModifiedVersion);
    }
}