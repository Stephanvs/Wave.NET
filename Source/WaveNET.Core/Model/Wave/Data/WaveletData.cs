using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Version;

namespace WaveNET.Core.Model.Wave.Data
{
    public class WaveletData : AbstractWaveletData<IBlipData>
    {
        private readonly IList<ParticipantId> _participants = new Collection<ParticipantId>();
        private readonly IDictionary<string, IBlipData> _documents = new Dictionary<string, IBlipData>(); 

        public WaveletData(WaveletId waveletId, ParticipantId creator, DateTime creationTime, long version,
            HashedVersion hashedVersion, DateTime lastModifiedTime, WaveId waveId)
            : base(waveletId, creator, creationTime, version, hashedVersion, lastModifiedTime, waveId)
        {
        }

        public WaveletData(IReadableWaveletData data) : base(data)
        {
        }

        public override IBlipData GetDocument(string documentName)
        {
            throw new NotImplementedException();
        }

        public override ICollection<string> GetDocumentIds()
        {
            throw new NotImplementedException();
        }

        protected override IList<ParticipantId> GetMutableParticipants()
        {
            return _participants;
        }
    }
}