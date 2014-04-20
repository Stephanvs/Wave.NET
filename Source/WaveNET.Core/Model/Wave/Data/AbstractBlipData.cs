using System;
using System.Collections.ObjectModel;

namespace WaveNET.Core.Model.Wave.Data
{
    public abstract class AbstractBlipData : IBlipData
    {
        private readonly string _id;
        private readonly AbstractWaveletData<IBlipData> _wavelet;
        private DateTime _lastModifiedTime;
        private long _lastModifiedVersion;

        protected AbstractBlipData(string id, AbstractWaveletData<IBlipData> wavelet, ParticipantId author,
                                   IDocumentOperationSink content, DateTime lastModifiedTime, long lastModifiedVersion)
        {
            Content = content;
            Id = id;
            Author = author;
            _wavelet = wavelet;
            _lastModifiedTime = lastModifiedTime;
            _lastModifiedVersion = lastModifiedVersion;
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

        DateTime IReadableBlipData.LastModifiedTime
        {
            get { return _lastModifiedTime; }
        }

        long IBlipData.LastModifiedVersion
        {
            get { return _lastModifiedVersion; }
            set { _lastModifiedVersion = value; }
        }

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

        DateTime IBlipData.LastModifiedTime
        {
            get { return _lastModifiedTime; }
            set
            {
                if (value != _lastModifiedTime)
                {
                    _lastModifiedTime = value;
                }
            }
        }

        long IReadableBlipData.LastModifiedVersion
        {
            get { return _lastModifiedVersion; }
        }

        public IDocumentOperationSink Content { get; private set; }

        public string Id { get; private set; }
    }
}