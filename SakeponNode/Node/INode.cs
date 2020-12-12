using System.Collections.Generic;

namespace SakeponNode
{

    /// <summary>
    /// ツリーにおけるノード
    /// </summary>
    /// <typeparam name="T">ノードの値Type</typeparam>
    public interface INode<T>
    {
        /// <summary>
        /// 子ノードを作成し追加する
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="id">ID *nullable</param>
        /// <returns>追加された子ノード</returns>
        INode<T> AppendChild(T value, string id = null);

        /// <summary>
        /// IDを指定して子ノードを削除する
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Self</returns>
        INode<T> RemoveChild(string id);

        /// <summary>
        /// ノードを指定して子ノードを削除する
        /// </summary>
        /// <param name="child">ノード</param>
        /// <returns>Self</returns>
        INode<T> RemoveChild(INode<T> child);

        /// <summary>
        /// 親ノードを取得（無ければNull）
        /// </summary>
        INode<T> Parent { get; }

        /// <summary>
        /// 最初の子ノードを取得（無ければNull）
        /// </summary>
        INode<T> FirstChild { get; }

        /// <summary>
        /// 最後の子ノードを取得（無ければNull）
        /// </summary>
        INode<T> LastChild { get; }

        /// <summary>
        /// 次の兄弟ノードを取得（無ければNull）
        /// </summary>
        INode<T> NextSibling { get; }

        /// <summary>
        /// 前の兄弟ノードを取得（無ければNull）
        /// </summary>
        INode<T> PrevSibling { get; }

        /// <summary>
        /// IDを指定して子ノードが存在しているかを確認する
        /// </summary>
        /// <param name="id">ノードID</param>
        /// <returns>存在するか</returns>
        bool ExistChild(string id);

        /// <summary>
        /// ノードを指定して子ノードが存在しているかを確認する
        /// </summary>
        /// <param name="child">ノード</param>
        /// <returns>存在するか</returns>
        bool ExistChild(INode<T> child);

        /// <summary>
        /// 自身を削除する
        /// </summary>
        void RemoveSelf();

        /// <summary>
        /// 親ノードが存在するか(自身はルートではない)を取得する
        /// </summary>
        bool HasParent { get; }

        /// <summary>
        /// 子ノードを取得する
        /// </summary>
        IReadOnlyList<INode<T>> Children { get; }

        /// <summary>
        /// 子ノードを含む自身の子孫全てを直列化して返す
        /// </summary>
        IEnumerable<INode<T>> Descendants { get; }

        /// <summary>
        /// 値
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// ノードID
        /// </summary>
        string Id { get; set; }
    }
}