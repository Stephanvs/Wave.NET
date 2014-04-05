using System;

namespace WaveNET.Core.Model.Document.Operation
{
    public abstract class DocOpComponent
    {
        public new abstract DocOpComponentType GetType();
        public abstract void Apply(IDocOpCursor cursor);
    }

    public abstract class DocInitializationComponent : DocOpComponent
    {
        public override void Apply(IDocOpCursor cursor)
        {
            Apply(cursor);
        }

        public abstract void Apply(IDocInitializationCursor cursor);
    }

    public class AnnotationBoundary : DocInitializationComponent
    {
        private readonly IAnnotationBoundaryMap _boundary;

        public AnnotationBoundary(IAnnotationBoundaryMap boundary)
        {
            _boundary = boundary;
        }

        public IAnnotationBoundaryMap Boundary
        {
            get { return _boundary; }
        }

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.AnnotationBoundary;
        }

        public override void Apply(IDocInitializationCursor cursor)
        {
            cursor.AnnotationBoundary(_boundary);
        }
    }

    public class Characters : DocInitializationComponent
    {
        private readonly String _characters;

        public Characters(String characters)
        {
            _characters = characters;
        }

        public String ChangedCharacters
        {
            get { return _characters; }
        }

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.Characters;
        }

        public override void Apply(IDocInitializationCursor cursor)
        {
            cursor.Characters(_characters);
        }
    }

    public class ElementStart : DocInitializationComponent
    {
        private readonly IAttributes _attributes;
        private readonly string _type;

        public ElementStart(string type, IAttributes attributes)
        {
            _type = type;
            _attributes = attributes;
        }

        public string Type
        {
            get { return _type; }
        }

        public IAttributes Attributes
        {
            get { return _attributes; }
        }

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.ElementStart;
        }

        public override void Apply(IDocInitializationCursor cursor)
        {
            cursor.ElementStart(_type, _attributes);
        }
    }

    public class ElementEnd : DocInitializationComponent
    {
        public static ElementEnd Instance = new ElementEnd();

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.ElementEnd;
        }

        public override void Apply(IDocInitializationCursor cursor)
        {
            cursor.ElementEnd();
        }
    }

    public class Retain : DocOpComponent
    {
        private readonly int _itemCount;

        public Retain(int itemCount)
        {
            _itemCount = itemCount;
        }

        public int ItemCount
        {
            get { return _itemCount; }
        }

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.Retain;
        }

        public override void Apply(IDocOpCursor cursor)
        {
            cursor.Retain(_itemCount);
        }
    }

    public class DeleteCharacters : DocOpComponent
    {
        private readonly string _characters;

        public DeleteCharacters(string characters)
        {
            _characters = characters;
        }

        public string Characters
        {
            get { return _characters; }
        }

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.DeleteCharacters;
        }

        public override void Apply(IDocOpCursor cursor)
        {
            cursor.DeleteCharacters(_characters);
        }
    }

    public class DeleteElementStart : DocOpComponent
    {
        private readonly IAttributes _attributes;
        private readonly string _type;

        public DeleteElementStart(string type, IAttributes attributes)
        {
            _type = type;
            _attributes = attributes;
        }

        public string Type
        {
            get { return _type; }
        }

        public IAttributes Attributes
        {
            get { return _attributes; }
        }

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.DeleteElementStart;
        }

        public override void Apply(IDocOpCursor cursor)
        {
            cursor.DeleteElementStart(_type, _attributes);
        }
    }

    public class DeleteElementEnd : DocOpComponent
    {
        public static DeleteElementEnd Instance = new DeleteElementEnd();

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.DeleteElementEnd;
        }

        public override void Apply(IDocOpCursor cursor)
        {
            cursor.DeleteElementEnd();
        }
    }

    public class ReplaceAttributes : DocOpComponent
    {
        private readonly IAttributes _newAttributes;
        private readonly IAttributes _oldAttributes;

        public ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
        {
            _oldAttributes = oldAttributes;
            _newAttributes = newAttributes;
        }

        public IAttributes OldAttributes
        {
            get { return _oldAttributes; }
        }

        public IAttributes NewAttributes
        {
            get { return _newAttributes; }
        }

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.ReplaceAttributes;
        }

        public override void Apply(IDocOpCursor cursor)
        {
            cursor.ReplaceAttributes(_oldAttributes, _newAttributes);
        }
    }

    public class UpdateAttributes : DocOpComponent
    {
        private readonly IAttributesUpdate _attributesUpdate;

        public UpdateAttributes(IAttributesUpdate attributesUpdate)
        {
            _attributesUpdate = attributesUpdate;
        }

        public IAttributesUpdate AttributesUpdated
        {
            get { return _attributesUpdate; }
        }

        public override DocOpComponentType GetType()
        {
            return DocOpComponentType.UpdateAttributes;
        }

        public override void Apply(IDocOpCursor cursor)
        {
            cursor.UpdateAttributes(_attributesUpdate);
        }
    }
}