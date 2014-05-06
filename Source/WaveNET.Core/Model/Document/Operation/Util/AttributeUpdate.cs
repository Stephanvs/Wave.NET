using System;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Document.Operation.Util
{
    public class AttributeUpdate
    {
        public AttributeUpdate(String name, String oldValue, String newValue)
        {
            Preconditions.CheckNotNull(name, "Null name in AttributeUpdate");

            Name = name;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string NewValue { get; set; }

        public string OldValue { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return "[" + Name + ": " + OldValue + " -> " + NewValue + "]";
        }
    }
}