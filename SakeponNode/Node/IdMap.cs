using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace SakeponNode.Node
{
    public class IdMap<T> : ConcurrentDictionary<string, INode<T>>
    {
    }
}
