using System;
using System.Collections.ObjectModel;

namespace WaveNET.Core.Model.Wave.Data
{
    public abstract class AbstractBlipData : IBlipData
    {
        private readonly AbstractWaveletData<IBlipData> _wavelet;

        protected AbstractBlipData(string id, AbstractWaveletData<IBlipData> wavelet, ParticipantId author,
                                   IDocumentOperationSink content, DateTime lastModifiedTime, long lastModifiedVersion)
        {
            Content = content;
            Id = id;
            Author = author;
            _wavelet = wavelet;
            LastModifiedTime = lastModifiedTime;
            LastModifiedVersion = lastModifiedVersion;
        }

        public IWaveletData Wavelet
        {
            get { return _wavelet; }
        }

        IReadableWaveletData IReadableBlipData.Wavelet
        {
            get { return _wavelet; }
        }

        public ParticipantId Author { get; private set; }

        public abstract ReadOnlyCollection<ParticipantId> Contributors { get; }

        public DateTime LastModifiedTime { get; set; }

        public long LastModifiedVersion { get; set; }

        public void Submit()
        {
            throw new NotImplementedException();
        }

        public abstract void AddContributor(ParticipantId participant);

        public abstract void RemoveContributor(ParticipantId participant);

        public void OnRemoteContentModified()
        {
            // todo: call _wavelet.getListenerManager().onRemoteBlipDataContentModified(_wavelet, this);
        }

        public IDocumentOperationSink Content { get; private set; }

        public string Id { get; private set; }
    }
}