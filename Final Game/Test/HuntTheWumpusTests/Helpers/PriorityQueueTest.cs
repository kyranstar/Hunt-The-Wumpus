using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuntTheWumpusTests.Helpers
{
    [TestClass]
    public class PriorityQueueTest
    {
        [TestMethod]
        public void TestPriorityQueueOrder()
        {
            int SIZE = 10;
            HeapPriorityQueue<Node> q = new HeapPriorityQueue<Node>(SIZE);
            q.Enqueue(new Node("a"), 0);
            q.Enqueue(new Node("b"), 1);

            Assert.AreEqual("a", q.Dequeue().Str);
            Assert.AreEqual("b", q.Dequeue().Str);
        }
    }

    class Node : PriorityQueueNode
    {
        public string Str;
        public Node(string s)
        {
            Str = s;
        }
    }
}
