using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WaveNET.Core.Model.Version;
using WaveNET.Core.Model.Wave;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    /// <summary>
    ///     This is pretty much like a no-op except it updates the version information.
    ///     It also contains a docId when it wants to update the meta-data of a document.
    /// </summary>
    public sealed class VersionUpdateOperation
        : WaveletOperation
    {
        private readonly string _docId;
        private readonly long _docVersion;
        private readonly bool _useFixedBlipInfo;

        /// <summary>
        ///     Creates a new instance of <see cref="VersionUpdateOperation" />
        /// </summary>
        /// <param name="creator">creator to add or remove as a contributor</param>
        /// <param name="versionIncrement">version increment when the operation is applied</param>
        /// <param name="hashedVersion">distinct version after the operation is applied</param>
        /// <param name="docId">optional document whose version is to be updated</param>
        /// <param name="docVersion">
        ///     if <paramref name="useFixedBlipInfo" />, the last modified version to apply to blip being
        ///     updated
        /// </param>
        /// <param name="useFixedBlipInfo">whether to use the remaining two fields</param>
        public VersionUpdateOperation(ParticipantId creator, long versionIncrement, HashedVersion hashedVersion,
                                      string docId = null, long docVersion = -1, bool useFixedBlipInfo = false)
            : base(new WaveletOperationContext(creator, new DateTime(), versionIncrement, hashedVersion))
        {
            _docId = docId;
            _docVersion = docVersion;
            _useFixedBlipInfo = useFixedBlipInfo;
        }

        protected override void DoApply(IWaveletData wavelet)
        {
            DoApplyInternal(wavelet);
        }

        private VersionUpdateOperation DoApplyInternal(IWaveletData wavelet)
        {
            HashedVersion oldHashedVersion = wavelet.HashedVersion;
            if (string.IsNullOrEmpty(_docId))
            {
                // Update blip version.
                var blip = (IBlipData) wavelet.GetDocument(_docId);
                long newWaveletVersion = wavelet.Version + Context.VersionIncrement;
                long newDocVersion = _useFixedBlipInfo ? _docVersion : newWaveletVersion;
                long oldDocVersion = blip.LastModifiedVersion = newDocVersion;

                return new VersionUpdateOperation(Context.Creator, -Context.VersionIncrement, oldHashedVersion, _docId,
                    oldDocVersion, true);
            }
            return new VersionUpdateOperation(Context.Creator, -Context.VersionIncrement, oldHashedVersion);
        }

        public override IList<WaveletOperation> ApplyAndReturnReverse(IWaveletData target)
        {
            var result = new ReadOnlyCollection<WaveletOperation>(new WaveletOperation[]
            {
                DoApplyInternal(target)
            });
            Update(target);

            return result;
        }

        public override void AcceptVisitor(IWaveletOperationVisitor visitor)
        {
            visitor.VisitVersionUpdateOperation(this);
        }
    }
}