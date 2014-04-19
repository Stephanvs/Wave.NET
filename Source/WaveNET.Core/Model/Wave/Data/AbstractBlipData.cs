using System;
using System.Collections.ObjectModel;

namespace WaveNET.Core.Model.Wave.Data
{
    public abstract class AbstractBlipData : IBlipData
    {
        IReadableWaveletData IReadableBlipData.Wavelet
        {
            get { return Wavelet; }
        }

        DateTime IBlipData.LastModifiedTime
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        long IBlipData.LastModifiedVersion
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public abstract void Submit();
        public abstract void AddContributor(ParticipantId participant);
        public abstract void RemoveContributor(ParticipantId participant);
        public abstract void OnRemoteContentModified();
        public abstract IWaveletData Wavelet { get; }
        public abstract ParticipantId Author { get; }
        public abstract ReadOnlyCollection<ParticipantId> Contributors { get; }

        DateTime IReadableBlipData.LastModifiedTime
        {
            get { throw new NotImplementedException(); }
        }

        long IReadableBlipData.LastModifiedVersion
        {
            get { throw new NotImplementedException(); }
        }

        public abstract IDocumentOperationSink Content { get; }
        public abstract string Id { get; }
    }
}