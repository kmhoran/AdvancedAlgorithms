using Microsoft.VisualStudio.TestTools.UnitTesting;
using Structures.Enums;
using System;
using System.Collections;

namespace Structures.Test
{
    [TestClass]
    public class SortedPriorityQueueTest
    {
        [TestMethod]
        public void Constructor_produces_emptyQueue()
        {
            var queue = new SortedPriorityQueue<string>();
            Assert.AreEqual(queue.Count, 0);
        }

        [TestMethod]
        public void Insert_with_varietyOrValues_maintainsOrder()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("n", 1);
            queue.Insert("a", 2);
            queue.Insert("z", 3);
            queue.Insert("w", 4);
            queue.Insert("u", 2);

            Assert.AreEqual("n", queue.Top());
            Assert.AreEqual("u", queue.Top());
            Assert.AreEqual("a", queue.Top());
            Assert.AreEqual("z", queue.Top());
            Assert.AreEqual("w", queue.Top());
        }

        [TestMethod]
        public void Insert_with_newHighestValue_updatesQueueRoot()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("b", 2);
            queue.Insert("c", 3);
            queue.Insert("d", 4);

            Assert.AreEqual("b", queue.Peek());

            queue.Insert("a", 1);

            Assert.AreEqual("a", queue.Peek());
        }

        [TestMethod]
        public void Max_withEmptyQueue_throwsError()
        {
            var queue = new SortedPriorityQueue<string>();

            bool threwError = false;
            try
            {
                queue.Max();
            }
            catch (Exception e)
            {

                Assert.AreEqual("Cannot find max of empty queue.", e.Message);
                threwError = true;
            }

            Assert.IsTrue(threwError);
        }

        [TestMethod]
        public void Max_withVarietyOfElements_returnsMax()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("n", 1);
            queue.Insert("a", 2);
            Assert.AreEqual("n", queue.Max());

            queue.Insert("u", 2);
            Assert.AreEqual("u", queue.Max());

            queue.Insert("z", 3);
            Assert.AreEqual("z", queue.Max());

            queue.Insert("w", 4);
            Assert.AreEqual("z", queue.Max());
        }


        [TestMethod]
        public void Min_withEmptyQueue_throwsError()
        {
            var queue = new SortedPriorityQueue<string>();

            bool threwError = false;
            try
            {
                queue.Min();
            }
            catch (Exception e)
            {

                Assert.AreEqual("Cannot find min of empty queue.", e.Message);
                threwError = true;
            }

            Assert.IsTrue(threwError);
        }

        [TestMethod]
        public void Min_withVarietyOfElements_returnsMax()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("n", 1);
            queue.Insert("z", 2);
            Assert.AreEqual("n", queue.Min());

            queue.Insert("k", 2);
            Assert.AreEqual("k", queue.Min());

            queue.Insert("a", 3);
            Assert.AreEqual("a", queue.Min());

            queue.Insert("d", 4);
            Assert.AreEqual("a", queue.Min());
        }

        [TestMethod]
        public void Peek_withEmptyQueue_throwsError()
        {
            var queue = new SortedPriorityQueue<string>();

            bool threwError = false;
            try
            {
                queue.Peek();
            }
            catch (Exception e)
            {

                Assert.AreEqual("Cannot peek empty queue.", e.Message);
                threwError = true;
            }

            Assert.IsTrue(threwError);
        }

        [TestMethod]
        public void Peek_withVarietyOfInserts_returnsTopPriority()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("n", 5);
            queue.Insert("z", 4);
            Assert.AreEqual("z", queue.Peek());

            queue.Insert("k", 3);
            Assert.AreEqual("k", queue.Peek());

            queue.Insert("a", 2);
            Assert.AreEqual("a", queue.Peek());

            queue.Insert("d", 10);
            Assert.AreEqual("a", queue.Peek());
        }

        [TestMethod]
        public void Remove_withNoMatch_doesNotAffectQueueSize()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("n", 1);
            queue.Insert("a", 2);
            queue.Insert("z", 3);
            queue.Insert("w", 4);
            queue.Insert("u", 2);

            Assert.AreEqual(5, queue.Count);
            queue.Remove("y");
            Assert.AreEqual(5, queue.Count);
        }

        [TestMethod]
        public void Remove_withNullParameter_doesNotAffectQueueSize()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("n", 1);

            Assert.AreEqual(1, queue.Count);
            queue.Remove(null);
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void Remove_withVariousValues_reducesCount_andMaintainsOrderAndPriority()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("n", 1);
            queue.Insert("a", 2);
            queue.Insert("z", 3);
            queue.Insert("w", 2);
            queue.Insert("k", 4);

            Assert.AreEqual("z", queue.Max());
            Assert.AreEqual(5, queue.Count);
            queue.Remove("z");
            Assert.AreEqual("w", queue.Max());
            Assert.AreEqual(4, queue.Count);

            Assert.AreEqual("a", queue.Min());
            queue.Remove("a");
            Assert.AreEqual("k", queue.Min());
            Assert.AreEqual(3, queue.Count);

            Assert.AreEqual("n", queue.Peek());
            queue.Remove("n");
            Assert.AreEqual("w", queue.Peek());
            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        public void Remove_withEmptyQueue_throwsError()
        {
            var queue = new SortedPriorityQueue<string>();

            bool threwError = false;
            try
            {
                queue.Remove("r");
            }
            catch (Exception e)
            {

                Assert.AreEqual("Cannot remove from empty queue.", e.Message);
                threwError = true;
            }

            Assert.IsTrue(threwError);
        }

        [TestMethod]
        public void Top_withVariousValues_reducesCount_andMaintainsOrderAndPriority()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("a", 2);
            queue.Insert("n", 1);
            queue.Insert("z", 3);


            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual("n", queue.Top());
            Assert.AreEqual(2, queue.Count);

            queue.Insert("w", 2);
            queue.Insert("k", 4);
            Assert.AreEqual(4, queue.Count);
            Assert.AreEqual("a", queue.Top());
            Assert.AreEqual("w", queue.Top());
            Assert.AreEqual("z", queue.Top());
            Assert.AreEqual("k", queue.Top());
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void Top_withEmptyQueue_throwsError()
        {
            var queue = new SortedPriorityQueue<string>();

            bool threwError = false;
            try
            {
                queue.Top();
            }
            catch (Exception e)
            {

                Assert.AreEqual("Cannot get top of empty queue.", e.Message);
                threwError = true;
            }

            Assert.IsTrue(threwError);
        }

        [TestMethod]
        public void Update_withEmptyQueue_throwsError()
        {
            var queue = new SortedPriorityQueue<string>();

            bool threwError = false;
            try
            {
                queue.Update("a", 1);
            }
            catch (Exception e)
            {

                Assert.AreEqual("Cannot update empty queue.", e.Message);
                threwError = true;
            }

            Assert.IsTrue(threwError);
        }

        [TestMethod]
        public void Update_withHigherPriority_bubblesElementUpHeap()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("a", 5);
            queue.Insert("b", 3);
            queue.Insert("c", 6);

            Assert.AreEqual("b", queue.Peek());
            Assert.AreEqual(3, queue.Count);
            queue.Update("c", 2);
            Assert.AreEqual("c", queue.Peek());
            Assert.AreEqual(3, queue.Count);

            queue.Insert("d", 5);
            queue.Insert("e", 3);
            queue.Insert("f", 7);

            Assert.AreEqual(6, queue.Count);
            queue.Update("f", 1);
            Assert.AreEqual("f", queue.Peek());
            Assert.AreEqual(6, queue.Count);
        }

        [TestMethod]
        public void Update_withLowerPriority_pushedElementDown()
        {
            var queue = new SortedPriorityQueue<string>();

            queue.Insert("a", 0);
            queue.Insert("b", 0);
            queue.Insert("c", 0);
            queue.Insert("d", 0);
            queue.Insert("e", 0);
            queue.Insert("f", 0);

            Assert.AreEqual(6, queue.Count);
            queue.Update("a", 4);
            queue.Update("b", 2);
            queue.Update("c", 5);
            queue.Update("d", 1);
            queue.Update("e", 3);
            queue.Update("f", 6);

            Assert.AreEqual(6, queue.Count);
            Assert.AreEqual("d", queue.Top());
            Assert.AreEqual("b", queue.Top());
            Assert.AreEqual("e", queue.Top());
            Assert.AreEqual("a", queue.Top());
            Assert.AreEqual("c", queue.Top());
            Assert.AreEqual("f", queue.Top());
            Assert.AreEqual(0, queue.Count);
        }
    }
}