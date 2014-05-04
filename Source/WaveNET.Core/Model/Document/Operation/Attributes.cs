using System.Collections.Generic;

namespace WaveNET.Core.Model.Document.Operation
{
    public class Attributes
        : Dictionary<string, string>
        , IAttributes
    {
        public static readonly IAttributes Empty = new Attributes();
        
        public IAttributes UpdateWith(IAttributesUpdate mutation)
        {
            throw new System.NotImplementedException();
        }

        public IAttributes UpdateWithNoCompatibilityCheck(IAttributesUpdate mutation)
        {
            throw new System.NotImplementedException();
        }
    }
}