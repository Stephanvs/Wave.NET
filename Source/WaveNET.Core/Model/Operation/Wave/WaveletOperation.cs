using System.Collections.Generic;
using System.Diagnostics.Contracts;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    public abstract class WaveletOperation
        : IReversableOperation<WaveletOperation, IWaveletData>
            , IVisitable<IWaveletOperationVisitor>
    {
        public WaveletOperation(WaveletOperationContext context)
        {
            Contract.Requires(Context != null);

            Context = context;
        }

        /// <summary>
        ///     Context metadata which does not affect the logic of an operation.
        /// </summary>
        public WaveletOperationContext Context { get; private set; }

        /// <summary>
        ///     This method delegates the operation logic to <see cref="DoApply(WaveletData)" />
        /// </summary>
        /// <param name="wavelet"></param>
        public void Apply(IWaveletData wavelet)
        {
            // Execute subtype logic first, because if the subtype logic throws an exception, we must
            // leave this wrapper untouched as though the operation never happened. The subtype is
            // responsible for making sure if they throw an exception they must leave themselves in a
            // state as if the op never happened.
            DoApply(wavelet);

            // Update metadata second. This means subtype subtypes should assume that the
            // metadata of a wavelet will be at the old state if they look at it in their
            // operation logic.
            Update(wavelet);
        }

        public abstract IList<WaveletOperation> ApplyAndReturnReverse(IWaveletData target);

        public abstract void AcceptVisitor(IWaveletOperationVisitor visitor);

        public void Update(IWaveletData wavelet)
        {
            if (Context.HasTimestamp)
            {
                wavelet.LastModifiedTime = Context.Timestamp;
            }

            if (Context.VersionIncrement != 0)
            {
                wavelet.Version = wavelet.Version + Context.VersionIncrement;
            }

            if (Context.HasHashedVersion)
            {
                wavelet.HashedVersion = Context.HashedVersion;
            }
        }

        /// <summary>
        ///     Applies this operation's logic to a given wavelet. This method can be
        ///     arbitrarily overridden by subclasses.
        /// </summary>
        /// <param name="wavelet">Wavelet on which this operation is to apply itself</param>
        protected abstract void DoApply(IWaveletData wavelet);

        protected virtual string SuffixForToString()
        {
            return string.Format("by {0} at {1} version {2}", Context.Creator, Context.Timestamp, Context.HashedVersion);
        }

        protected WaveletOperationContext CreateReverseContext(IWaveletData target, long versionDecrement)
        {
            return new WaveletOperationContext(Context.Creator, Context.LastModifiedTime, -versionDecrement,
                target.HashedVersion);
        }

        protected WaveletOperationContext CreateReverseContext(IWaveletData target)
        {
            return CreateReverseContext(target, Context.VersionIncrement);
        }

        /// <summary>
        ///     Whether this operation worthy of attribution. Subclasses may override this.
        /// </summary>
        /// <returns>This default implementation always returns true.</returns>
        public virtual bool IsWorthyOfAttribution()
        {
            return true;
        }
    }
}