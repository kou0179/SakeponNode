using SakeponNode.Exceptions;
using SakeponNode.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakeponNode
{
    public class Node
    {
        public static Node<T> CreateRootNode<T>(T value, string id = null)
        {
            return new Node<T>()
            {
                Value = value,
                Id = id,
                IdMap = new IdMap<T>()
            };
        }
    }

    public class Node<T> : INode<T>
    {
        public INode<T> Parent { get; internal set; }

        private List<INode<T>> _children = new List<INode<T>>();

        internal Node()
        {
        }

        public IReadOnlyList<INode<T>> Children => _children;
        public T Value { get; set; }
        public string Id { get; set; }

        public INode<T> FirstChild => Children.FirstOrDefault();

        public INode<T> LastChild => Children.FirstOrDefault();

        public INode<T> NextSibling
        {
            get
            {
                if (Parent == null)
                {
                    throw new IsRootNodeException("Cannot get sibling nodes due to parent node.");
                }
                try
                {
                    var result = Parent.Children[Parent.Children.IndexOf(this) + 1];
                    return result;
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }
            }
        }

        public INode<T> PrevSibling
        {
            get
            {
                if (Parent == null)
                {
                    throw new IsRootNodeException("Cannot get sibling nodes due to parent node.");
                }
                try
                {
                    var result = Parent.Children[Parent.Children.IndexOf(this) - 1];
                    return result;
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }
            }
        }

        public INode<T> AppendChild(T value, string id = null)
        {
            var child = new Node<T>()
            {
                Parent = this,
                Value = value,
                Id = id,
                IdMap = this.IdMap
            };

            if (child.Id != null && !IdMap.TryAdd(child.Id, child))
            {
                throw new ArgumentException("Duplicate id", nameof(id));
            }

            _children.Add(child);

            return child;
        }

        public bool ExistChild(string id)
        {
            return _children.Select(node => node.Id).Contains(id);
        }

        public bool HasParent => Parent != null;

        public INode<T> RemoveChild(string id)
        {
            if (!ExistChild(id))
            {
                throw new ArgumentException("Non-existent child", nameof(id));
            }
            _children.Remove(_children.First(child => child.Id == id));
            IdMap.TryRemove(id, out _);
            return this;
        }

        public INode<T> RemoveChild(INode<T> child)
        {
            _children = _children.Where(_c => _c != child).ToList();
            (child as Node<T>).Parent = null;
            child.RemoveSelf();
            return this;
        }

        public bool ExistChild(INode<T> child)
        {
            return _children.Any(_c => _c != child);
        }

        public void RemoveSelf()
        {
            var underIds = Descendants.Where(c => c.Id != null).Select(c => c.Id);
            foreach (var id in underIds)
            {
                IdMap.TryRemove(id, out _);
            }
            if (HasParent)
            {
                Parent.RemoveChild(this);
            }
        }

        internal IdMap<T> IdMap { get; set; }

        public IEnumerable<INode<T>> Descendants => GetDescendants();

        private IEnumerable<INode<T>> GetDescendants()
        {
            List<INode<T>> descendants = new List<INode<T>>();
            descendants = Recursion(this, descendants).GetAwaiter().GetResult();
            return descendants;

            async Task<List<INode<T>>> Recursion(INode<T> node, List<INode<T>> list)
            {
                var result = list;
                foreach (var child in node.Children)
                {
                    result = list.Concat(await Recursion(child, list)).ToList();
                }
                return result;
            }
        }
    }
}