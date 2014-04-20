using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WaveNET.Core.Model.Wave.Data
{
    public class BlipData : AbstractBlipData
    {
        private readonly IList<ParticipantId> _contributors;

        public BlipData(string docId, WaveletData waveletData, ParticipantId author, IList<ParticipantId> contributors, IDocumentOperationSink contentSink, DateTime lastModifiedTime, long lastModifiedVersion)
            : base(docId, waveletData, author, contentSink, lastModifiedTime, lastModifiedVersion)
        {
            _contributors = contributors;
        }

        public override ReadOnlyCollection<ParticipantId> Contributors
        {
            get { return new ReadOnlyCollection<ParticipantId>(_contributors); }
        }

        public override void AddContributor(ParticipantId participant)
        {
            _contributors.Add(participant);
        }

        public override void RemoveContributor(ParticipantId participant)
        {
            if (_contributors.Contains(participant))
            {
                _contributors.Remove(participant);
            }
        }
    }
}