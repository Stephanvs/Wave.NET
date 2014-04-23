using System;
using System.Collections.Generic;
using WaveNET.Core.Model.Operation;

namespace WaveNET.Core.Model.Document.Operation.Algorithm
{
    public class Composer
    {
        private readonly static IEvaluatingDocOpCursor<IDocOp> _normalizer;
        private static Target _target;
        private readonly Target _defaultTarget = new DefaultPreTarget();
        private static AnnotationQueue _preAnnotationQueue;
        private static AnnotationQueue _postAnnotationQueue;

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
                            + "op1 resulting length=" + DocOpUtil.resultingDocumentLength(op1)
                            + ", op2 initial length=" + DocOpUtil.initialDocumentLength(op2));
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
        /// Returns the composition of two operations.
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
                foreach (var annotationBoundaryMap in _events)
                {
                    Unqueue(annotationBoundaryMap);
                }
                _events.Clear();
            }
        }

        private abstract class Target 
            : IDocOpCursor
        {
            public abstract bool IsPostTarget();
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

        private class RetainPostTarget
            : PostTarget
        {
            public RetainPostTarget(int itemCount)
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
    }
}