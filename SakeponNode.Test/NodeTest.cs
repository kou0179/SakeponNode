using Microsoft.VisualStudio.TestTools.UnitTesting;
using SakeponNode;

namespace SakeponNode.Test
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void CreateRootNode_Test()
        {
            var node = Node.CreateRootNode("test", "1");
            Assert.AreEqual("test", node.Value);
            Assert.AreEqual("1", node.Id);
        }

        [TestMethod]
        public void AppendChild_Test()
        {
            var rootNode = Node.CreateRootNode("test", "0");
            var childNode = rootNode.AppendChild("hoge", "0-1");
            Assert.AreEqual("hoge", childNode.Value);
            Assert.AreEqual("0-1", childNode.Id);
            Assert.AreEqual(rootNode.Children.Count, 1);
            Assert.AreEqual(childNode.Parent,rootNode);
        }

        [TestMethod]
        public void RemoveChild_ById_Test()
        {
            var rootNode = Node.CreateRootNode("test", "0");
            var childNode = rootNode.AppendChild("hoge", "0-1");
            rootNode.RemoveChild("0-1");
            Assert.AreEqual(rootNode.Children.Count, 0);
        }
    }
}
