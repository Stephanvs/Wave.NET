using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WaveNET.Core.Model.Id;
using WaveNET.Core.Model.Version;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Wave.Data
{
    public class WaveletData 
        : AbstractWaveletData<IBlipData>
    {
        public class WaveletDataFactory 
            : IReadableWaveletDataFactory<IWaveletData>
        {
            private readonly IDocumentFactory _documentFactory;

            private WaveletDataFactory(IDocumentFactory documentFactory)
            {
                Preconditions.CheckNotNull(documentFactory, "null DocumentFactory");
                _documentFactory = documentFactory;
            }

            public static WaveletDataFactory Create(IDocumentFactory documentFactory) {
                return new WaveletDataFactory(documentFactory);
            }

            public IWaveletData Create(IReadableWaveletData data)
            {
                var waveletData = new WaveletData(data, _documentFactory);
                // Todo: implement copy methods
                //waveletData.CopyParticipants(data);
                //waveletData.CopyDocuments(data);
                return waveletData;
            }
        }

        private readonly IList<ParticipantId> _participants = new Collection<ParticipantId>();
        private readonly IDictionary<string, IBlipData> _documents = new Dictionary<string, IBlipData>(); 

        public WaveletData(WaveletId waveletId, ParticipantId creator, DateTime creationTime, long version,
            HashedVersion hashedVersion, DateTime lastModifiedTime, WaveId waveId, IDocumentFactory factory)
            : base(waveletId, creator, creationTime, version, hashedVersion, lastModifiedTime, waveId, factory)
        {
        }

        public WaveletData(IReadableWaveletData data, IDocumentFactory factory)
            : base(data, factory)
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

        protected override IBlipData InternalCreateDocument(string docId, ParticipantId author, ICollection<ParticipantId> contributors,
            IDocumentOperationSink contentSink, DateTime lastModifiedTime, long lastModifiedVersion)
        {
            Preconditions.CheckArgument(!_documents.ContainsKey(docId), "Duplicate doc id: %s", docId);

            var blip = new BlipData(docId, this, author, contributors, contentSink, lastModifiedTime, lastModifiedVersion);
            _documents.Add(docId, blip);
            return blip;
        }
    }
}