using System.Collections.Generic;
using System.Collections.ObjectModel;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Document.Operation.Algorithm;
using WaveNET.Core.Model.Wave.Data;

namespace WaveNET.Core.Model.Operation.Wave
{
    /// <summary>
    ///     Operation class for boxing a document operation as a wave/blip operation.
    ///     A <see cref="BlipContentOperation" /> applies to a blip by passing its contained document operation to
    ///     the blip's document-operation sink.  Note that this is slightly different from what one might
    ///     expect (applying to a blip by applying the contained document-operation to the blip's content),
    ///     but it leverages the weaker contract of a sink in order to hide the document structure from the
    ///     blip interface such that a wider range of implementation options are possible for the reception
    ///     of document operations.
    /// </summary>
    public class BlipContentOperation
        : BlipOperation
    {
        public IDocOp ContentOp { get; set; }

        private readonly UpdateContributorMethod _method;

        public BlipContentOperation(WaveletOperationContext context, IDocOp contentOp, UpdateContributorMethod update = UpdateContributorMethod.Add)
            : base(context)
        {
            ContentOp = contentOp;
            _method = update;
        }

        public override IList<BlipOperation> ApplyAndReturnReverse(IBlipData target)
        {
            var reverseContext = CreateReverseContext(target);
            
            // Update metadata
            var reverseMethod = Update(target, _method);

            target.Content.Consume(ContentOp);

            var reverseContentOp = DocOpInverter<IDocOp>.Invert(ContentOp);

            var reverseOp = new BlipContentOperation(reverseContext, reverseContentOp, reverseMethod);
            
            return new ReadOnlyCollection<BlipOperation>(new BlipOperation[] { reverseOp });
        }

        public override void AcceptVisitor(IBlipOperationVisitor visitor)
        {
            visitor.VisitBlipContentOperation(this);
        }

        protected override void DoApply(IBlipData target)
        {
            target.Content.Consume(ContentOp);

            if (IsWorthyOfAttribution(target.Id))
            {
                target.OnRemoteContentModified();
            }
        }

        protected override void DoUpdate(IBlipData target)
        {
            Update(target, _method);
        }

        protected override bool UpdatesBlipMetadata(string blipId)
        {
            return IsWorthyOfAttribution(blipId);
        }
    }
}