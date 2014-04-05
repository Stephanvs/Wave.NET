using System.Collections.Generic;

namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     An implementation of <see cref="IEvaluatingDocOpCursor" /> that buffers the operation
    ///     and returns it as a <see cref="IBufferedDocOp" />.
    ///     See also <see cref="DocOpBuilder" />, which is similar but implements a standard
    ///     Java builder pattern instead of IEvaluatingDocOpCursor.
    ///     The operation returned is not guaranteed to be well-formed, as there is no
    ///     check upon completion by default.
    ///     Use sparingly. Its use should only be for ill-formedness tests, and possibly
    ///     for efficiency if profiling reveals it to be necessary. If in doubt, use
    ///     DocOpBuffer which does a well-formedness check.
    /// </summary>
    public class UncheckedDocOpBuffer : IEvaluatingDocOpCursor<IBufferedDocOp>
    {
        private readonly List<DocOpComponent> _components = new List<DocOpComponent>();

        /// <summary>
        ///     Behaviour is undefined if this buffer is used after calling this method.
        /// </summary>
        public IBufferedDocOp Finish()
        {
            return FinishUnchecked();
        }

        public void Retain(int itemCount)
        {
            _components.Add(new Retain(itemCount));
        }

        public void DeleteCharacters(string characters)
        {
            _components.Add(new DeleteCharacters(characters));
        }

        public void DeleteElementStart(string type, IAttributes attributes)
        {
            _components.Add(new DeleteElementStart(type, attributes));
        }

        public void DeleteElementEnd()
        {
            _components.Add(Operation.DeleteElementEnd.Instance);
        }

        public void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
        {
            _components.Add(new ReplaceAttributes(oldAttributes, newAttributes));
        }

        public void UpdateAttributes(IAttributesUpdate attributesUpdate)
        {
            _components.Add(new UpdateAttributes(attributesUpdate));
        }

        public void AnnotationBoundary(IAnnotationBoundaryMap map)
        {
            _components.Add(new AnnotationBoundary(map));
        }

        public void Characters(string characters)
        {
            _components.Add(new Characters(characters));
        }

        public void ElementStart(string type, IAttributes attributes)
        {
            _components.Add(new ElementStart(type, attributes));
        }

        public void ElementEnd()
        {
            _components.Add(Operation.ElementEnd.Instance);
        }

        /// <summary>
        ///     Finish and do a well formedness check as well
        ///     Behaviour is undefined if this buffer is used after calling this method.
        /// </summary>
        public IBufferedDocOp FinishChecked()
        {
            return BufferedDocOp.Create(_components);
        }

        /// <summary>
        ///     <see cref="Finish" />
        ///     This is dangerous; we currently use it for ill-formedness-detection
        ///     tests, and may use it for efficiency in other places in the future.
        /// </summary>
        /// <returns></returns>
        public IBufferedDocOp FinishUnchecked()
        {
            return BufferedDocOp.CreateUnchecked(_components);
        }
    }
}