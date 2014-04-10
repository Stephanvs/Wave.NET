using System;
using System.Collections.Generic;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    /// <summary>
    ///     Operation class for a particular kind of wavelet operation that does something to a blip within a wavelet.
    /// </summary>
    public abstract class BlipOperation
        : IReversableOperation<BlipOperation, IBlipData>
            , IVisitable<IBlipOperationVisitor>
    {
        public enum UpdateContributorMethod
        {
            /// <summary>
            /// Will not alter the list
            /// </summary>
            None,
            /// <summary>
            /// Will add the author, if not already present (ie. is a MaybeAdd)
            /// </summary>
            Add,
            /// <summary>
            /// Will remove the author, if already present (ie. is a MaybeRemove)
            /// </summary>
            Remove
        }

        public BlipOperation(WaveletOperationContext context)
        {
            Context = context;
        }

        public WaveletOperationContext Context { get; protected set; }

        /// <summary>
        /// Applies the logic in <see cref="Apply"/> to a blip, and updates its metadata.
        /// </summary>
        /// <remarks>This should not be invoked directly by subclasses; use <see cref="DoApply"/> instead.</remarks>
        /// <param name="target"></param>
        public void Apply(IBlipData target)
        {
            // Execute subtype logic first, because if the subtype logic throws an exception, we must
            // leave this wrapper untouched as though the operation never happened. The subtype is
            // responsible for making sure if they throw an exception they must leave themselves in a
            // state as those the op never happened.
            DoApply(target);

            // Update metadata second. This means subtypes should assume that the
            // metadata of a blip will be at the old state if they look at it in their
            // operation logic.
            DoUpdate(target);
        }

        public abstract IList<BlipOperation> ApplyAndReturnReverse(IBlipData target);

        public abstract void AcceptVisitor(IBlipOperationVisitor visitor);

        /// <summary>
        ///     Mutates a blip.  Subclasses can arbitrarily override this to execute their logic.
        /// </summary>
        /// <param name="target">blip to modify</param>
        protected abstract void DoApply(IBlipData target);

        /// <summary>
        ///     Updates the metadata of a blip.  An operation is free to choose whether or
        ///     not if affects the metadata of a blip.  If it does, it may find the
        ///     contributor-handling method {@link #update(BlipData, UpdateContributorMethod)} useful.
        /// </summary>
        /// <param name="target">blip to update</param>
        protected abstract void DoUpdate(IBlipData target);

        public virtual bool IsWorthyOfAttribution(string blipId)
        {
            return true;
        }

        protected abstract bool UpdatesBlipMetadata(string blipId);

        /// <summary>
        ///     Creates the operation context for the reverse of an operation.
        /// </summary>
        /// <param name="target">blip from which to extract state to be restored by the reverse operation</param>
        /// <param name="versionDecrement">Number of versions to decrement in reverse.</param>
        /// <returns>context for a reverse of this operation.</returns>
        protected WaveletOperationContext CreateReverseContext(IBlipData target, long versionDecrement)
        {
            // For now, we don't care about version numbers with reverse context
            return new WaveletOperationContext(Context.Creator, target.LastModifiedTime, -versionDecrement);
        }

        /// <summary>
        ///     Creates the operation context for the reverse of an operation.
        /// </summary>
        /// <param name="target">blip from which to extract state to be restored by the reverse operation</param>
        /// <returns>context for a reverse of this operation.</returns>
        protected WaveletOperationContext CreateReverseContext(IBlipData target)
        {
            return CreateReverseContext(target, Context.VersionIncrement);
        }

        protected UpdateContributorMethod Update(IBlipData target, UpdateContributorMethod method)
        {
            if (!UpdatesBlipMetadata(target.Id))
            {
                return UpdateContributorMethod.None;
            }

            UpdateContributorMethod reverse;
            switch (method)
            {
                case UpdateContributorMethod.Add:
                {
                    throw new NotImplementedException();
                    break;
                }
                case UpdateContributorMethod.Remove:
                {
                    throw new NotImplementedException();
                    break;
                }
                default:
                {
                    reverse = UpdateContributorMethod.None;
                    break;
                }
            }

            target.LastModifiedVersion = target.Wavelet.Version + Context.VersionIncrement;
            if (Context.HasTimestamp)
            {
                target.LastModifiedTime = Context.Timestamp;
            }

            return reverse;
        }
    }
}