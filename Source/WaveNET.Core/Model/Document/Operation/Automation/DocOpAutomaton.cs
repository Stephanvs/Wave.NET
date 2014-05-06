using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WaveNET.Core.Model.Document.Operation.Automation
{
    // Todo: rename to DocOpAutomaton
    public class DocOpAutomaton
    {
        public static readonly IAutomatonDocument EmptyDocument = new EmptyDocumentAutomaton();
        private readonly IDocumentSchema _constraints;
        private readonly IAutomatonDocument _document;
        private readonly IAnnotationsUpdate _annotationsUpdate = new AnnotationsUpdate();
        private readonly IList<InsertStart> _insertionStack = new List<InsertStart>();
        private int _deletionStackDepth = 0;
        private int _effectivePos = 0;
        private int _resultingPos = 0;

        /// <summary>
        ///     Creates an automaton that corresponds to the set of all possible operations
        ///     on the given document under the given schema constraints.
        /// </summary>
        public DocOpAutomaton(IAutomatonDocument document, IDocumentSchema constraints)
        {
            _document = document;
            _constraints = constraints;
        }

        public ValidationResult CheckCharacters(string characters, ViolationCollector collector)
        {
            if (characters == null) return NullCharacters(collector);
            if (string.IsNullOrEmpty(characters)) return EmptyCharacters(collector);

            // Todo: implement other checks

            return Valid();
        }

        private ValidationResult AddViolation(ViolationCollector a, OperationIllFormed v)
        {
            if (a != null)
            {
                a.Add(v);
            }
            return v.ValidationResult;
        }

        private ValidationResult AddViolation(ViolationCollector a, OperationInvalid v)
        {
            if (a != null)
            {
                a.Add(v);
            }
            return v.ValidationResult;
        }

        private ValidationResult AddViolation(ViolationCollector a, SchemaViolation v)
        {
            if (a != null)
            {
                a.Add(v);
            }
            return v.ValidationResult;
        }

        private OperationIllFormed IllFormedOperation(string description)
        {
            return new OperationIllFormed(description, _effectivePos, _resultingPos);
        }

        private OperationInvalid InvalidOperation(string description)
        {
            return new OperationInvalid(description, _effectivePos, _resultingPos);
        }

        private SchemaViolation SchemaViolation(string description)
        {
            return new SchemaViolation(description, _effectivePos, _resultingPos);
        }

        private ValidationResult Valid()
        {
            return ValidationResult.Valid;
        }

        private ValidationResult NullCharacters(ViolationCollector collector)
        {
            return AddViolation(collector, IllFormedOperation("characters is null"));
        }

        private ValidationResult EmptyCharacters(ViolationCollector collector)
        {
            return AddViolation(collector, IllFormedOperation("characters is empty"));
        }

        private ValidationResult RetainItemCountNotPositive(ViolationCollector v)
        {
            return AddViolation(v, IllFormedOperation("retain item count not positive"));
        }

        private ValidationResult MismatchedInsertStart(ViolationCollector v)
        {
            return AddViolation(v, IllFormedOperation("elementStart with no matching elementEnd"));
        }

        private ValidationResult MismatchedDeleteStart(ViolationCollector v)
        {
            return AddViolation(v, IllFormedOperation("deleteElementStart with no matching deleteElementEnd"));
        }

        private ValidationResult MismatchedStartAnnotation(ViolationCollector v, String key)
        {
            return AddViolation(v, IllFormedOperation("annotation of key " + key + " starts but never ends"));
        }

        private ValidationResult MismatchedEndAnnotation(ViolationCollector v, String key)
        {
            return AddViolation(v, IllFormedOperation("annotation of key " + key + " ends without having started"));
        }

        private ValidationResult MissingRetainToEnd(ViolationCollector v, int expectedLength, int actualLength)
        {
            return AddViolation(v, InvalidOperation("operation shorter than document, document length "
                + expectedLength + ", length of input of operation " + actualLength));
        }

        private ValidationResult RetainInsideInsertOrDelete(ViolationCollector v)
        {
            return AddViolation(v, IllFormedOperation("retain inside insert or delete"));
        }

        public ValidationResult CheckRetain(int itemCount, ViolationCollector v)
        {
            // well-formedness
            if (itemCount <= 0)
            {
                return RetainItemCountNotPositive(v);
            }
            if (!InsertionStackIsEmpty()) { return RetainInsideInsertOrDelete(v); }
            if (!DeletionStackIsEmpty()) { return RetainInsideInsertOrDelete(v); }
            //// validity
            if (!CanRetain(itemCount))
            {
                return RetainPastEnd(v, _document.Length(), itemCount);
            }
            return CheckAnnotationsForRetain(v, itemCount);
        }

        private ValidationResult CheckAnnotationsForRetain(ViolationCollector collector, int itemCount)
        {
            for (int i = 0; i < _annotationsUpdate.ChangeSize(); i++)
            {
                throw new NotImplementedException();
                //var key = _annotationsUpdate.getChangeKey(i);
                //var oldValue = _annotationsUpdate.getOldValue(i);
                //var firstChange = _document.FirstAnnotationChange(_effectivePos, _effectivePos + itemCount, key, oldValue);
                //if (firstChange != -1)
                //{
                //    return OldAnnotationsDifferFromDocument(collector, key, oldValue,
                //        _document.GetAnnotation(firstChange, key));
                //}
            }
            return Valid();
        }

        private ValidationResult RetainPastEnd(ViolationCollector collector, int expectedLength, int retainItemCount)
        {
            return AddViolation(collector,
                InvalidOperation("retain past end of document, document length " + expectedLength +
                                 ", retain item count " + retainItemCount));
        }

        private bool CanRetain(int itemCount)
        {
            Debug.Assert(itemCount >= 0);
            return itemCount <= MaxRetainItemCount();
        }

        private int MaxRetainItemCount()
        {
            if (_effectivePos >= _document.Length())
            {
                return 0;
            }

            return _document.Length() - _effectivePos;
        }

        public void DoRetain(int itemCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks whether the automaton is in an accepting state, i.e., whether the
        /// operation would be valid if no further operation components follow.
        /// </summary>
        public ValidationResult CheckFinish(ViolationCollector v)
        {
            // well-formedness
            if (!InsertionStackIsEmpty())
            {
                foreach (var e in _insertionStack)
                {
                    return e.NotClosed(this, v);
                }
            }
            if (!DeletionStackIsEmpty())
            {
                return MismatchedDeleteStart(v);
            }
            if (_annotationsUpdate.ChangeSize() > 0)
            {
                return MismatchedStartAnnotation(v, _annotationsUpdate.GetChangeKey(0));
            }

            // validity
            if (_effectivePos != _document.Length())
            {
                return MissingRetainToEnd(v, _document.Length(), _effectivePos);
            }
            return Valid();
        }

        private bool InsertionStackIsEmpty()
        {
            return ! _insertionStack.Any();
        }

        private bool DeletionStackIsEmpty()
        {
            return _deletionStackDepth == 0;
        }

        private class InsertStart
        {
            private readonly string _tag;

            private InsertStart(string tag)
            {
                _tag = tag;
            }

            public static InsertStart GetInstance(String tag)
            {
                Debug.Assert(tag != null);
                return new InsertStart(tag);
            }

            internal ValidationResult NotClosed(DocOpAutomaton automaton, ViolationCollector collector)
            {
                return automaton.MismatchedInsertStart(collector);
            }
        }
    }
}