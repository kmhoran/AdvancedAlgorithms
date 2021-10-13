using Microsoft.VisualStudio.TestTools.UnitTesting;
using Structures.Enums;
using System;
using System.Collections;

namespace Structures.Test
{
    [TestClass]
    public class PriorityQueueTest
    {
        public readonly PriorityQueueOptions maxOptions = new()
        {
            HeapType = HeapType.MaxHeap,
            MaxLeafCount = 3
        };
        public readonly PriorityQueueOptions minOptions = new()
        {
            HeapType = HeapType.MinHeap,
            MaxLeafCount = 3
        };

        [TestMethod]
        public void Constructor_with_noParameters_producesEmptyQueue()
        {
            var queue = new PriorityQueue<string>();
            Assert.IsTrue(queue.Count == 0);
        }

        [TestMethod]
        public void Constructor_maxHeap_with_parameters_producesOrederedQueue()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("high", 10), ("low", 1), ("medium", 5) }, maxOptions);
            Assert.IsTrue(queue.Count > 0);
            Assert.AreEqual(queue.Top(), "high");
            Assert.AreEqual(queue.Top(), "medium");
            Assert.AreEqual(queue.Top(), "low");
        }
        public void Constructor_minHeap_with_parameters_producesOrederedQueue()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("high", 1), ("low", 10), ("medium", 5) }, minOptions);
            Assert.IsTrue(queue.Count > 0);
            Assert.AreEqual(queue.Top(), "high");
            Assert.AreEqual(queue.Top(), "medium");
            Assert.AreEqual(queue.Top(), "low");
        }

        [TestMethod]
        public void Insert_maxheap_withVarietyOfProorities_insertsNodes_andMaintainsOrder()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5) }, maxOptions);
            queue.Insert("three", 3);
            queue.Insert("seven", 7);
            queue.Insert("negative one", -1);
            queue.Insert("nine-nine", 99);

            Assert.AreEqual(queue.Top(), "nine-nine");
            Assert.AreEqual(queue.Top(), "ten");
            Assert.AreEqual(queue.Top(), "seven");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "three");
            Assert.AreEqual(queue.Top(), "one");
            Assert.AreEqual(queue.Top(), "negative one");
        }

        [TestMethod]
        public void Insert_minheap_withVarietyOfProorities_insertsNodes_andMaintainsOrder()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5) }, minOptions);
            queue.Insert("three", 3);
            queue.Insert("seven", 7);
            queue.Insert("negative one", -1);
            queue.Insert("nine-nine", 99);

            Assert.AreEqual(queue.Top(), "negative one");
            Assert.AreEqual(queue.Top(), "one");
            Assert.AreEqual(queue.Top(), "three");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "seven");
            Assert.AreEqual(queue.Top(), "ten");
            Assert.AreEqual(queue.Top(), "nine-nine");
        }

        [TestMethod]
        public void Peek_returnsHighestPriority_andDoesNotChangeQueueSize()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("high", 10), ("low", 1), ("medium", 5) });
            int prevCount = queue.Count;
            var peeked = queue.Peek();
            Assert.AreEqual(queue.Count, prevCount);
            Assert.AreEqual(queue.Top(), peeked);
        }

        [TestMethod]
        public void Remove_maxheap_with_nonMatchingSearchElement_DoesNotChangeQueue()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5) }, maxOptions);

            queue.Remove("seven");

            Assert.AreEqual(queue.Top(), "ten");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "one");
        }

        [TestMethod]
        public void Remove_minheap_with_nonMatchingSearchElement_DoesNotChangeQueue()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5) }, minOptions);

            queue.Remove("seven");

            Assert.AreEqual(queue.Top(), "one");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "ten");
        }


        [TestMethod]
        public void Remove_maxheap_with_matchingSearchElement_maintainsOrder()
        {
            var queue = new PriorityQueue<string>(
                new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5), ("three", 3), ("seven", 7), ("negative one", -1), ("nine-nine", 99) },
                maxOptions);

            queue.Remove("seven");

            Assert.AreEqual(queue.Top(), "nine-nine");
            Assert.AreEqual(queue.Top(), "ten");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "three");
            Assert.AreEqual(queue.Top(), "one");
            Assert.AreEqual(queue.Top(), "negative one");
        }

        [TestMethod]
        public void Remove_minheap_with_matchingSearchElement_maintainsOrder()
        {
            var queue = new PriorityQueue<string>(
                new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5), ("three", 3), ("seven", 7), ("negative one", -1), ("nine-nine", 99) },
                minOptions);

            queue.Remove("seven");

            Assert.AreEqual(queue.Top(), "negative one");
            Assert.AreEqual(queue.Top(), "one");
            Assert.AreEqual(queue.Top(), "three");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "ten");
            Assert.AreEqual(queue.Top(), "nine-nine");
        }

        [TestMethod]
        public void Remove_maxheap_with_multipleMatches_removesOneNode_andMaintainsOrder()
        {
            var queue = new PriorityQueue<string>(
                new (string, int)[] { ("three", 3), ("five", 5), ("five", 5), ("five", 5), ("seven", 7), ("five", 5), ("nine-nine", 99) },
                maxOptions);

            queue.Remove("seven");

            Assert.AreEqual(queue.Top(), "nine-nine");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "three");
        }

        [TestMethod]
        public void Remove_minheap_with_multipleMatches_removesOneNode_andMaintainsOrder()
        {
            var queue = new PriorityQueue<string>(
                new (string, int)[] { ("three", 3), ("five", 5), ("five", 5), ("five", 5), ("seven", 7), ("five", 5), ("nine-nine", 99) },
                minOptions);

            queue.Remove("seven");

            Assert.AreEqual(queue.Top(), "three");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "nine-nine");
        }

        [TestMethod]
        public void Remove_with_emptyQueue_throwsError()
        {
            bool threwError = false;
            try
            {
                var queue = new PriorityQueue<string>();
                queue.Remove(string.Empty);
            }
            catch (ApplicationException e)
            {
                Assert.AreEqual(e.Message, "Cannot Remove from empty PriorityQueue");
                threwError = true;
            }
            Assert.IsTrue(threwError);
        }

        [TestMethod]
        public void Top_maxheap_returnsHighestPriorityNode()
        {
            var queue = new PriorityQueue<string>(
                new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5) },
                maxOptions);

            Assert.AreEqual(queue.Top(), "ten");

            queue.Insert("fifteen", 15);
            queue.Insert("twenty", 20);
            queue.Insert("sixteen", 16);
            queue.Insert("three", 3);

            Assert.AreEqual(queue.Top(), "twenty");
            Assert.AreEqual(queue.Top(), "sixteen");
            Assert.AreEqual(queue.Top(), "fifteen");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "three");
            Assert.AreEqual(queue.Top(), "one");
            Assert.IsTrue(queue.Count == 0);
        }

        [TestMethod]
        public void Top_minheap_returnsHighestPriorityNode()
        {
            var queue = new PriorityQueue<string>(
                new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5) },
                minOptions);

            Assert.AreEqual(queue.Top(), "one");

            queue.Insert("fifteen", 15);
            queue.Insert("twenty", 20);
            queue.Insert("sixteen", 16);
            queue.Insert("three", 3);

            Assert.AreEqual(queue.Top(), "three");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "ten");
            Assert.AreEqual(queue.Top(), "fifteen");
            Assert.AreEqual(queue.Top(), "sixteen");
            Assert.AreEqual(queue.Top(), "twenty");

            Assert.IsTrue(queue.Count == 0);
        }

        [TestMethod]
        public void Top_with_emptyQueue_throwsError()
        {
            bool threwError = false;
            try
            {
                var queue = new PriorityQueue<string>();
                queue.Top();
            }
            catch (ApplicationException e)
            {
                Assert.AreEqual(e.Message, "Cannot Top empty PriorityQueue");
                threwError = true;
            }
            Assert.IsTrue(threwError);
        }

        [TestMethod]
        public void Update_maxheap_with_nonMatchingSearchElement_doesNotChangeQueue()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5) }, maxOptions);

            queue.Update("seven", 7);

            Assert.AreEqual(queue.Top(), "ten");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "one");
        }

        [TestMethod]
        public void Update_minheap_with_nonMatchingSearchElement_doesNotChangeQueue()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5) }, minOptions);

            queue.Update("seven", 7);

            Assert.AreEqual(queue.Top(), "one");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "ten");
        }

        [TestMethod]
        public void Update_maxheap_with_matchingSearchElement_maintainsOrder()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("ten", 10), ("one", 1), ("five", 5), ("wild", 15) }, maxOptions);

            Assert.AreEqual(queue.Peek(), "wild");

            queue.Update("wild", 3);

            Assert.AreEqual(queue.Top(), "ten");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "wild");
            Assert.AreEqual(queue.Top(), "one");
        }

        [TestMethod]
        public void Update_minheap_with_matchingSearchElement_maintainsOrder()
        {
            var queue = new PriorityQueue<string>(new (string, int)[] { ("ten", 10), ("three", 3), ("five", 5), ("wild", 1) }, minOptions);

            Assert.AreEqual(queue.Peek(), "wild");

            queue.Update("wild", 7);

            Assert.AreEqual(queue.Top(), "three");
            Assert.AreEqual(queue.Top(), "five");
            Assert.AreEqual(queue.Top(), "wild");
            Assert.AreEqual(queue.Top(), "ten");

        }

        [TestMethod]
        public void Update_with_emptyQueue_throwsError()
        {
            bool threwError = false;
            try
            {
                var queue = new PriorityQueue<string>();
                queue.Update(string.Empty, 0);
            }
            catch (ApplicationException e)
            {
                Assert.AreEqual(e.Message, "Cannot Update empty PriorityQueue");
                threwError = true;
            }
            Assert.IsTrue(threwError);
        }

    }
}
