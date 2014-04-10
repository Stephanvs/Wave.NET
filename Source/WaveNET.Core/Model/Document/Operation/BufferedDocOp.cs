using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using WaveNET.Core.Model.Document.Operation.Automation;
using WaveNET.Core.Model.Document.Operation.Validation;

namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    ///     Namespace private. Use of of the following to construct a buffered doc op:
    ///     <see cref="DocOpBuilder" />
    ///     <see cref="DocInitializationBuilder" />
    ///     <see cref="DocOpBuffer" />
    ///     <see cref="DocInitializationBuffer" />
    /// </summary>
    public class BufferedDocOp : IBufferedDocOp
    {
        #region BufferedDocOp Implementation

        private readonly IList<DocOpComponent> _components;
        private bool _knownToBeWellFormed = false;

        private BufferedDocOp(IList<DocOpComponent> components)
        {
            _components = components;
        }

        public int Size()
        {
            return _components.Count;
        }

        public DocOpComponentType GetType(int i)
        {
            return _components[i].GetType();
        }

        public void ApplyComponent(int i, IDocOpCursor cursor)
        {
            _components[i].Apply(cursor);
        }

        public IAnnotationBoundaryMap GetAnnotationBoundary(int i)
        {
            Check(i, DocOpComponentType.AnnotationBoundary);
            return ((AnnotationBoundary) _components[i]).Boundary;
        }

        public string GetCharactersString(int i)
        {
            Check(i, DocOpComponentType.Characters);
            return ((Characters) _components[i]).ChangedCharacters;
        }

        public string GetElementStartTag(int i)
        {
            Check(i, DocOpComponentType.ElementStart);
            return ((ElementStart) _components[i]).Type;
        }

        public IAttributes GetElementStartAttributes(int i)
        {
            Check(i, DocOpComponentType.ElementStart);
            return ((ElementStart) _components[i]).Attributes;
        }

        public int GetRetainItemCount(int i)
        {
            Check(i, DocOpComponentType.Retain);
            return ((Retain) _components[i]).ItemCount;
        }

        public string GetDeleteCharactersString(int i)
        {
            Check(i, DocOpComponentType.DeleteCharacters);
            return ((DeleteCharacters) _components[i]).Characters;
        }

        public string GetDeleteElementStartTag(int i)
        {
            Check(i, DocOpComponentType.DeleteElementStart);
            return ((DeleteElementStart) _components[i]).Type;
        }

        public IAttributes GetDeleteElementStartAttributes(int i)
        {
            Check(i, DocOpComponentType.DeleteElementStart);
            return ((DeleteElementStart) _components[i]).Attributes;
        }

        public IAttributes GetReplaceAttributesOldAttributes(int i)
        {
            Check(i, DocOpComponentType.ReplaceAttributes);
            return ((ReplaceAttributes) _components[i]).OldAttributes;
        }

        public IAttributes GetReplaceAttributesNewAttributes(int i)
        {
            Check(i, DocOpComponentType.ReplaceAttributes);
            return ((ReplaceAttributes) _components[i]).NewAttributes;
        }

        public IAttributesUpdate GetUpdateAttributesUpdate(int i)
        {
            Check(i, DocOpComponentType.UpdateAttributes);
            return ((UpdateAttributes) _components[i]).AttributesUpdated;
        }

        public void Apply(IDocOpCursor cursor)
        {
            foreach (DocOpComponent item in _components)
                item.Apply(cursor);
        }

        private void Check(int i, DocOpComponentType type)
        {
            //Contract.Requires(
            //    _components[i].GetType() == type,
            //    String.Format("Component {0} is not of type '{1}' it is of type ''", i, type, _components[i].GetType())
            //    );
        }

        #region Overrides

        public override string ToString()
        {
            return DocOpUtil.ToConciseString(this);
        }

        #endregion

        #endregion

        #region Factory Methods

        public static BufferedDocOp Create(IList<DocOpComponent> components)
        {
            BufferedDocOp op = CreateUnchecked(components);
            CheckWellformedness(op);

            //Contract.Ensures(op._knownToBeWellFormed);
            return op;
        }

        public static BufferedDocOp CreateUnchecked(IList<DocOpComponent> components)
        {
            return new BufferedDocOp(components);
        }

        private static void CheckWellformedness(BufferedDocOp value)
        {
            if (!DocOpValidator.IsWellformed(null, (IBufferedDocOp) value))
            {
                // Check again, collecting violations this time.
                var violationCollector = new ViolationCollector();
                DocOpValidator.IsWellformed(violationCollector, (IBufferedDocOp) value);

                // Execution should not reach this point, because the DocOpValidator should return to caller
                //Contract.Ensures(true,
                //    String.Format("Attempt to build ill-formed operation ({0}): {1}", violationCollector, value));
            }
        }

        #endregion
    }
}