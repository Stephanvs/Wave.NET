using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Wave;

namespace WaveNET.Core.Model.Operation.Wave
{
    /// <summary>
    ///     A wavelet delta is a collection of <see cref="WaveletOperation" />s from a single author.
    /// </summary>
    public class WaveletDelta
    {
        /// <summary>
        ///     Author of the operations.
        /// </summary>
        private readonly ParticipantId _author;

        /// <summary>
        ///     List of operations in the order they are to be applied.
        /// </summary>
        private readonly IList<WaveletOperation> _operations;

        public WaveletDelta(ParticipantId author, IList<WaveletOperation> operations)
        {
            Contract.Ensures(author != null);
            Contract.Ensures(operations != null);
            Contract.Ensures(operations.Count > 0);

            _author = author;
            _operations = operations;
        }

        public IList<WaveletOperation> Operations
        {
            get { return new ReadOnlyCollection<WaveletOperation>(_operations.ToArray()); }
        }

        public override int GetHashCode()
        {
            // Todo: This is probably optimized for java, so replace with .NET equivalent.
            int result = 17;

            result = 31*result + _author.GetHashCode();
            foreach (WaveletOperation op in _operations)
                result = 31*result + op.GetHashCode();

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is WaveletDelta)
            {
                var wd = (WaveletDelta) obj;
                return _author.Equals(wd._author) && _operations.Equals(wd._operations);
            }
            return false;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("WaveletDelta(" + _author + ",");
            if (_operations.Count == 0)
            {
                stringBuilder.Append("[]");
            }
            else
            {
                stringBuilder.Append(" " + _operations.Count + " ops: [" + _operations[0]);
                for (int i = 1; i < _operations.Count; i++)
                {
                    stringBuilder.Append("," + _operations[i]);
                }
                stringBuilder.Append("]");
            }
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }
    }
}