using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SakeponNode.Exceptions
{
    [Serializable()]
    public class IsRootNodeException : InvalidOperationException
    {
        public IsRootNodeException()
            : base()
        {
        }

        public IsRootNodeException(string message)
            : base(message)
        {
        }

        public IsRootNodeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        //逆シリアル化コンストラクタ。このクラスの逆シリアル化のために必須。
        //アクセス修飾子をpublicにしないこと！（詳細は後述）
        protected IsRootNodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
