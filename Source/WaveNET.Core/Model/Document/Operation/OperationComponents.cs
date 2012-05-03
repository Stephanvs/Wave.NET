using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation
{
	public abstract class DocOpComponent
	{
		new public abstract DocOpComponentType GetType();
		public abstract void Apply(IDocOpCursor cursor);
	}

	public abstract class DocInitializationComponent : DocOpComponent
	{
		public override void Apply(IDocOpCursor cursor)
		{
			Apply((IDocInitializationCursor)cursor);
		}

		public abstract void Apply(IDocInitializationCursor cursor);
	}

	public class AnnotationBoundary : DocInitializationComponent
	{
		readonly IAnnotationBoundaryMap _boundary;

		public AnnotationBoundary(IAnnotationBoundaryMap boundary)
		{
			_boundary = boundary;
		}

		public override DocOpComponentType GetType()
		{
			return DocInitializationComponentType.AnnotationBoundary;
		}

		public override void Apply(IDocInitializationCursor cursor)
		{
			cursor.AnnotationBoundary(_boundary);
		}

		public IAnnotationBoundaryMap Boundary { get { return _boundary; } }
	}

	public class Characters : DocInitializationComponent
	{
		readonly String _characters;

		public Characters(String characters)
		{
			this._characters = characters;
		}

		public override DocOpComponentType GetType()
		{
			return DocInitializationComponentType.Characters;
		}

		public override void Apply(IDocInitializationCursor cursor)
		{
			cursor.Characters(_characters);
		}

		public String ChangedCharacters { get { return _characters; } }
	}

	public class ElementStart : DocInitializationComponent
	{
		readonly string _type;
		readonly IAttributes _attributes;

		public ElementStart(string type, IAttributes attributes)
		{
			_type = type;
			_attributes = attributes;
		}

		public override DocOpComponentType GetType()
		{
			return DocInitializationComponentType.ElementStart;
		}

		public override void Apply(IDocInitializationCursor cursor)
		{
			cursor.ElementStart(_type, _attributes);
		}

		public string Type { get { return _type; } }
		public IAttributes Attributes { get { return _attributes; } }
	}

	public class ElementEnd : DocInitializationComponent
	{
		public static ElementEnd Instance = new ElementEnd();

		public ElementEnd() { }

		public override DocOpComponentType GetType()
		{
			return DocInitializationComponentType.ElementEnd;
		}

		public override void Apply(IDocInitializationCursor cursor)
		{
			cursor.ElementEnd();
		}
	}

	public class Retain : DocOpComponent
	{
		readonly int _itemCount;

		public Retain(int itemCount)
		{
			_itemCount = itemCount;
		}

		public override DocOpComponentType GetType()
		{
			return DocOpComponentType.Retain;
		}

		public override void Apply(IDocOpCursor cursor)
		{
			cursor.Retain(_itemCount);
		}

		public int ItemCount { get { return _itemCount; } }
	}

	public class DeleteCharacters : DocOpComponent
	{
		readonly string _characters;

		public DeleteCharacters(string characters)
		{
			_characters = characters;
		}

		public override DocOpComponentType GetType()
		{
			return DocOpComponentType.DeleteCharacters;
		}

		public override void Apply(IDocOpCursor cursor)
		{
			cursor.DeleteCharacters(_characters);
		}

		public string Characters { get { return _characters; } }
	}

	public class DeleteElementStart : DocOpComponent
	{
		readonly string _type;
		readonly IAttributes _attributes;

		public DeleteElementStart(string type, IAttributes attributes)
		{
			_type = type;
			_attributes = attributes;
		}

		public override DocOpComponentType GetType()
		{
			return DocOpComponentType.DeleteElementStart;
		}

		public override void Apply(IDocOpCursor cursor)
		{
			cursor.DeleteElementStart(_type, _attributes);
		}

		public string Type { get { return _type; } }
		public IAttributes Attributes { get { return _attributes; } }
	}

	public class DeleteElementEnd : DocOpComponent
	{
		public static DeleteElementEnd Instance = new DeleteElementEnd();

		public DeleteElementEnd() { }

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
		readonly IAttributes _oldAttributes;
		readonly IAttributes _newAttributes;

		public ReplaceAttributes(IAttributes oldAttributes, IAttributes newAttributes)
		{
			_oldAttributes = oldAttributes;
			_newAttributes = newAttributes;
		}

		public override DocOpComponentType GetType()
		{
			return DocOpComponentType.ReplaceAttributes;
		}

		public override void Apply(IDocOpCursor cursor)
		{
			cursor.ReplaceAttributes(_oldAttributes, _newAttributes);
		}

		public IAttributes OldAttributes { get { return _oldAttributes; } }
		public IAttributes NewAttributes { get { return _newAttributes; } }
	}

	public class UpdateAttributes : DocOpComponent
	{
		readonly IAttributesUpdate _attributesUpdate;

		public UpdateAttributes(IAttributesUpdate attributesUpdate)
		{
			_attributesUpdate = attributesUpdate;
		}

		public override DocOpComponentType GetType()
		{
			return DocOpComponentType.UpdateAttributes;
		}

		public override void Apply(IDocOpCursor cursor)
		{
			cursor.UpdateAttributes(_attributesUpdate);
		}

		public IAttributesUpdate AttributesUpdated { get { return _attributesUpdate; } }
	}
}
