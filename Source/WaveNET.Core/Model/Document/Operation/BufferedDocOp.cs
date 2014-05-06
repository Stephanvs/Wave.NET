using System;
using System.Collections.Generic;
using WaveNET.Core.Model.Document.Operation.Automaton;
using WaveNET.Core.Model.Document.Operation.Validation;
using WaveNET.Core.Utils;

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
        private readonly IList<DocOpComponent> _components;

        private BufferedDocOp(IList<DocOpComponent> components)
        {
            IsKnownToBeWellformed = false;
            _components = components;
        }

        /// <summary>
        ///     true if the op is known to be well-formed. false implies nothing in particular.
        /// </summary>
        public bool IsKnownToBeWellformed { get; set; }

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

        private void Check(int i, DocOpComponentType expectedType)
        {
            DocOpComponentType actualType = _components[i].GetType();
            if (actualType != expectedType)
            {
                throw new ArgumentException("Component " + i + " is not of type ' " + expectedType + "', it is '" +
                                            actualType + "'");
            }
        }

        public override string ToString()
        {
            return DocOpUtil.ToConciseString(this);
        }

        public static BufferedDocOp Create(IList<DocOpComponent> components)
        {
            BufferedDocOp op = CreateUnchecked(components);
            CheckWellformedness(op);

            System.Diagnostics.Debug.Assert(op.IsKnownToBeWellformed);

            return op;
        }

        public static BufferedDocOp CreateUnchecked(IList<DocOpComponent> components)
        {
            return new BufferedDocOp(components);
        }

        /// <summary>
        /// Checks that a buffered doc op is well-formed.
        /// </summary>
        /// <param name="value">value op to check</param>
        /// <exception cref="InvalidOperationException">if the op is ill-formed</exception>
        private static void CheckWellformedness(IBufferedDocOp value)
        {
            if (!DocOpValidator.IsWellformed(null, value))
            {
                // Check again, collecting violations this time.
                var violationCollector = new ViolationCollector();
                DocOpValidator.IsWellformed(violationCollector, value);
                Preconditions.IllegalState(
                    string.Format("Attempt to build ill-formed operation ({0}): {1}", violationCollector, value));
            }
        }
    }
}