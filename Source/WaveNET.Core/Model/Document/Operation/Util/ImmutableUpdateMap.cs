using System.Collections.Generic;
using WaveNET.Core.Utils;

namespace WaveNET.Core.Model.Document.Operation.Util
{
    public class ImmutableUpdateMap<T, U>
        : IUpdateMap
    {
        private readonly IList<AttributeUpdate> _updates;

        public ImmutableUpdateMap()
        {
            _updates = new List<AttributeUpdate>();
        }

        public ImmutableUpdateMap(Dictionary<string, KeyValuePair<string, string>> updates)
            : this(TripletsFromDictionary(updates))
        {
        }

        public ImmutableUpdateMap(params string[] triples)
        {
            Preconditions.CheckArgument(triples.Length%3 == 0, "Triples must come in groups of three");

            var accu = new List<AttributeUpdate>(triples.Length/3);

            _updates = accu;
        }

        protected ImmutableUpdateMap(IList<AttributeUpdate> updates)
        {
            _updates = updates;
        } 

        public int ChangeSize()
        {
            return _updates.Count;
        }

        public string GetChangeKey(int changeIndex)
        {
            return _updates[changeIndex].Name;
        }

        public string GetOldValue(int changeIndex)
        {
            return _updates[changeIndex].OldValue;
        }

        public string GetNewValue(int changeIndex)
        {
            return _updates[changeIndex].NewValue;
        }

        public static string[] TripletsFromDictionary(IDictionary<string, KeyValuePair<string, string>> updates)
        {
            // Todo: Replace with Tuple<string, string, string>
            var triplets = new string[updates.Count*3];
            int i = 0;
            foreach (var e in  updates)
            {
                triplets[i++] = e.Key;
                triplets[i++] = e.Value.Key;
                triplets[i++] = e.Value.Value;
            }

            return triplets;
        }
    }
}