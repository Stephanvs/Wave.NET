using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WaveNET.Core.Model.Wave.Data
{
    public class BlipData : AbstractBlipData
    {
        public BlipData(string docId, WaveletData waveletData, ParticipantId author, ICollection<ParticipantId> contributors, IDocumentOperationSink contentSink, DateTime lastModifiedTime, long lastModifiedVersion)
        {
            throw new NotImplementedException();
        }

        public override void Submit()
        {
            throw new System.NotImplementedException();
        }

        public override void AddContributor(ParticipantId participant)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveContributor(ParticipantId participant)
        {
            throw new System.NotImplementedException();
        }

        public override void OnRemoteContentModified()
        {
            throw new System.NotImplementedException();
        }

        public override IWaveletData Wavelet
        {
            get { throw new System.NotImplementedException(); }
        }

        public override ParticipantId Author
        {
            get { throw new System.NotImplementedException(); }
        }

        public override ReadOnlyCollection<ParticipantId> Contributors
        {
            get { throw new System.NotImplementedException(); }
        }

        public override IDocumentOperationSink Content
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string Id
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}