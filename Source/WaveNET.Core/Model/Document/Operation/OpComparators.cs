using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace WaveNET.Core.Model.Document.Operation
{
    internal interface IOpEquator : IEqualityComparer<IDocOp>
    {
    }

    /// <summary>
    ///     Utilities for comparing operations.
    /// </summary>
    public class OpComparators
    {
        internal static readonly IOpEquator SyntacticIdentity = new SyntacticIdentityOpEquatable();

        private OpComparators()
        {
        }

        public bool Equals(IDocOp x, IDocOp y)
        {
            if (x == null) return y == null;
            if (y == null) return false;

            // TODO: Comparing by stringifying is unnecessarily expensive.
            return DocOpUtil.ToConciseString(x).Equals(DocOpUtil.ToConciseString(y));
        }

        public class SyntacticIdentityOpEquatable : IOpEquator
        {
            public bool Equals(IDocOp x, IDocOp y)
            {
                Contract.Requires(x != null, "x should not be null");
                Contract.Requires(y != null, "y should not be null");

                return EqualsNullable(x, y);
            }

            public int GetHashCode(IDocOp obj)
            {
                return obj.GetHashCode();
            }

            private bool EqualsNullable(IDocOp x, IDocOp y)
            {
                throw new NotImplementedException();
            }
        }
    }
}