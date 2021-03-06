﻿using System;
using System.Collections.Generic;
using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class Composer
    {
        private static IEvaluatingDocOpCursor<IDocOp> _normalizer;
        private static Target _target;
        private static readonly AnnotationQueue _preAnnotationQueue = new PreAnnotationQueue();
        private static readonly AnnotationQueue _postAnnotationQueue = new PostAnnotationQueue();
        private readonly Target _defaultTarget = new DefaultPreTarget();

        private Composer(IEvaluatingDocOpCursor<IDocOp> cursor)
        {
            _normalizer = OperationNormalizer.CreateNormalizer(cursor);
        }

        private IDocOp ComposeOperations(IDocOp op1, IDocOp op2)
        {
            _target = _defaultTarget;
            int op1Index = 0;
            int op2Index = 0;

            while (op1Index < op1.Size())
            {
                op1.ApplyComponent(op1Index++, _target);
                while (_target.IsPostTarget())
                {
                    if (op2Index >= op2.Size())
                    {
                        throw new OperationException("Document size mismatch: "
                                                     + "op1 resulting length=" + DocOpUtil.ResultingDocumentLength(op1)
                                                     + ", op2 initial length=" + DocOpUtil.InitialDocumentLength(op2));
                    }
                    op2.ApplyComponent(op2Index++, _target);
                }
            }
            if (op2Index < op2.Size())
            {
                _target = new FinisherPostTarget();
                while (op2Index < op2.Size())
                {
                    op2.ApplyComponent(op2Index++, _target);
                }
            }
            FlushAnnotations();
            return _normalizer.Finish();
        }

        /// <summary>
        ///     Returns the composition of two operations.
        /// </summary>
        /// <param name="first">the first operation</param>
        /// <param name="second">the second operation</param>
        /// <returns>the result of the composition</returns>
        /// <exception cref="OperationException">if applying op1 followed by op2 would be invalid</exception>
        public static IDocOp Compose(IDocOp first, IDocOp second)
        {
            try
            {
                return new Composer(new DocOpBuffer()).ComposeOperations(first, second);
            }
            catch (ComposeException exception)
            {
                throw new OperationException(exception.Message, exception);
            }
        }

        /// <summary>
        ///     Returns the composition of two operations, without checking whether the result is ill-formed.
        /// </summary>
        /// <remarks>
        ///     As mentioned in {@link UncheckedDocOpBuffer}, checked should only be used for testing or
        ///     when performance is a concern.
        /// </remarks>
        /// <param name="first">the first operation</param>
        /// <param name="second">the second operation</param>
        /// <returns>the result of the composition</returns>
        /// <exception cref="OperationException">if applying op1 followed by op2 would be invalid.</exception>
        public static IDocOp ComposeUnchecked(IDocOp first, IDocOp second)
        {
            try
            {
                return new Composer(new UncheckedDocOpBuffer()).ComposeOperations(first, second);
            }
            catch (ComposeException exception)
            {
                throw new OperationException(exception.Message, exception);
            }
        }

        private void FlushAnnotations()
        {
            _preAnnotationQueue.Flush();
            _postAnnotationQueue.Flush();
        }

        private abstract class AnnotationQueue
        {
            private readonly IList<IAnnotationBoundaryMap> _events = new List<IAnnotationBoundaryMap>();

            public abstract void Unqueue(IAnnotationBoundaryMap map);

            public void Enqueue(IAnnotationBoundaryMap map)
            {
                _events.Add(map);
            }

            public void Flush()
            {
                foreach (IAnnotationBoundaryMap annotationBoundaryMap in _events)
                {
                    Unqueue(annotationBoundaryMap);
                }
                _events.Clear();
            }
        }

        private class CharactersPostTarget
            : PostTarget
        {
            public CharactersPostTarget(string characters)
            {
                throw new NotImplementedException();
            }

            public override void Retain(int itemCount)
            {
                throw new NotImplementedException();
            }

            public override void DeleteCharacters(string characters)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementEnd()
            {
                throw new NotImplementedException();
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }
        }

        private class DefaultPreTarget
            : PreTarget
        {
            public override void Characters(string characters)
            {
                _target = new CharactersPostTarget(characters);
            }

            public override void ElementStart(string type, IAttributes attributes)
            {
                _target = new ElementStartPostTarget(type, attributes);
            }

            public override void ElementEnd()
            {
                _target = new ElementEndPostTarget();
            }

            public override void Retain(int itemCount)
            {
                _target = new RetainPostTarget(itemCount);
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                _target = new ReplaceAttributesPostTarget(oldAttributes, newAttributes);
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                _target = new UpdateAttributesPostTarget(attributesUpdate);
            }
        }

        private class DeleteCharactersPreTarget
            : PreTarget
        {
            public override void Characters(string characters)
            {
                throw new NotImplementedException();
            }

            public override void ElementStart(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void ElementEnd()
            {
                throw new NotImplementedException();
            }

            public override void Retain(int itemCount)
            {
                throw new NotImplementedException();
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }
        }

        private class ElementEndPostTarget
            : PostTarget
        {
            public override void Retain(int itemCount)
            {
                throw new NotImplementedException();
            }

            public override void DeleteCharacters(string characters)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementEnd()
            {
                throw new NotImplementedException();
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }
        }

        private class ElementStartPostTarget
            : PostTarget
        {
            public ElementStartPostTarget(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void Retain(int itemCount)
            {
                throw new NotImplementedException();
            }

            public override void DeleteCharacters(string characters)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementEnd()
            {
                throw new NotImplementedException();
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }
        }

        private class FinisherPostTarget
            : PostTarget
        {
            public override void Retain(int itemCount)
            {
                throw new ComposeException("Illegal composition");
            }

            public override void DeleteCharacters(string characters)
            {
                throw new ComposeException("Illegal composition");
            }

            public override void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new ComposeException("Illegal composition");
            }

            public override void DeleteElementEnd()
            {
                throw new ComposeException("Illegal composition");
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new ComposeException("Illegal composition");
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new ComposeException("Illegal composition");
            }
        }

        private class PostAnnotationQueue
            : AnnotationQueue
        {
            public override void Unqueue(IAnnotationBoundaryMap map)
            {
                throw new NotImplementedException();
            }
        }

        private abstract class PostTarget
            : Target
        {
            public override void Characters(string characters)
            {
                _postAnnotationQueue.Flush();
                _normalizer.Characters(characters);
            }

            public override void ElementStart(string type, IAttributes attributes)
            {
                _postAnnotationQueue.Flush();
                _normalizer.ElementStart(type, attributes);
            }

            public override void ElementEnd()
            {
                _postAnnotationQueue.Flush();
                _normalizer.ElementEnd();
            }

            public override void AnnotationBoundary(IAnnotationBoundaryMap map)
            {
                _postAnnotationQueue.Enqueue(map);
            }

            public override bool IsPostTarget()
            {
                return true;
            }
        }

        private class PreAnnotationQueue
            : AnnotationQueue
        {
            public override void Unqueue(IAnnotationBoundaryMap map)
            {
                throw new NotImplementedException();
            }
        }

        private abstract class PreTarget
            : Target
        {
            public override void DeleteCharacters(string characters)
            {
                _preAnnotationQueue.Flush();
                _normalizer.DeleteCharacters(characters);
            }

            public override void DeleteElementStart(string type, IAttributes attributes)
            {
                _preAnnotationQueue.Flush();
                _normalizer.DeleteElementStart(type, attributes);
            }

            public override void DeleteElementEnd()
            {
                _preAnnotationQueue.Flush();
                _normalizer.DeleteElementEnd();
            }

            public override void AnnotationBoundary(IAnnotationBoundaryMap map)
            {
                _preAnnotationQueue.Enqueue(map);
            }

            public override bool IsPostTarget()
            {
                return false;
            }
        }

        private class ReplaceAttributesPostTarget
            : PostTarget
        {
            public ReplaceAttributesPostTarget(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void Retain(int itemCount)
            {
                throw new NotImplementedException();
            }

            public override void DeleteCharacters(string characters)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementEnd()
            {
                throw new NotImplementedException();
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }
        }

        private class RetainPostTarget
            : PostTarget
        {
            private readonly int _itemCount;

            public RetainPostTarget(int itemCount)
            {
                _itemCount = itemCount;
            }

            public override void Retain(int itemCount)
            {
                throw new NotImplementedException();
            }

            public override void DeleteCharacters(string characters)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementEnd()
            {
                throw new NotImplementedException();
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }
        }

        private class RetainPreTarget
            : PreTarget
        {
            public override void Characters(string characters)
            {
                throw new NotImplementedException();
            }

            public override void ElementStart(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void ElementEnd()
            {
                throw new NotImplementedException();
            }

            public override void Retain(int itemCount)
            {
                throw new NotImplementedException();
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }
        }

        private abstract class Target
            : IDocOpCursor
        {
            public abstract void AnnotationBoundary(IAnnotationBoundaryMap map);
            public abstract void Characters(string characters);
            public abstract void ElementStart(string type, IAttributes attributes);
            public abstract void ElementEnd();
            public abstract void Retain(int itemCount);
            public abstract void DeleteCharacters(string characters);
            public abstract void DeleteElementStart(string type, IAttributes attributes);
            public abstract void DeleteElementEnd();
            public abstract void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes);
            public abstract void UpdateAttributes(IAttributesUpdate attributesUpdate);
            public abstract bool IsPostTarget();
        }

        private class UpdateAttributesPostTarget
            : PostTarget
        {
            public UpdateAttributesPostTarget(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }

            public override void Retain(int itemCount)
            {
                throw new NotImplementedException();
            }

            public override void DeleteCharacters(string characters)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementStart(string type, IAttributes attributes)
            {
                throw new NotImplementedException();
            }

            public override void DeleteElementEnd()
            {
                throw new NotImplementedException();
            }

            public override void ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
            {
                throw new NotImplementedException();
            }

            public override void UpdateAttributes(IAttributesUpdate attributesUpdate)
            {
                throw new NotImplementedException();
            }
        }
    }
}