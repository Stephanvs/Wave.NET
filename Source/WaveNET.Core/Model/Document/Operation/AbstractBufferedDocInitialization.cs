using System;

namespace WaveNET.Core.Model.Document.Operation
{
    public abstract class AbstractBufferedDocInitialization
        : AbstractDocInitialization
    {
        public override void Apply(IDocOpCursor cursor)
        {
            Apply((IDocInitializationCursor) cursor);
        }

        public override int GetRetainItemCount(int i)
        {
            throw new NotSupportedException("Initializations have no retain components");
        }

        public override string GetDeleteCharactersString(int i)
        {
            throw new NotSupportedException("Initializations have no delete characters components");
        }

        public override string GetDeleteElementStartTag(int i)
        {
            throw new NotSupportedException("Initializations have no delete element start components");
        }

        public override IAttributes GetDeleteElementStartAttributes(int i)
        {
            throw new NotSupportedException("Initializations have no delete element start components");
        }

        public override IAttributes GetReplaceAttributesOldAttributes(int i)
        {
            throw new NotSupportedException("Initializations have no replace attributes components");
        }

        public override IAttributes GetReplaceAttributesNewAttributes(int i)
        {
            throw new NotSupportedException("Initializations have no replace attributes components");
        }

        public override IAttributesUpdate GetUpdateAttributesUpdate(int i)
        {
            throw new NotSupportedException("Initializations have no update attributes components");
        }

        //public override string ToString()
        //{
        //    return "BufferedInit@" + int.ToHexString(System.IdentityHashCode(this)) + "[" +
        //        DocOpUtil.ToConciseString(DocOpScrub.MaybeScrub(this)) + "]";
        //}
    }
}