using System;

using System.Collections.Generic;
using System.Linq;
using Structures.Enums;
using Structures.Interfaces;

namespace Structures
{
    /**
    *  Priority queues are especially useful when we need to consume elements in a certain order 
    *    from a dynamically changing list (such as the list of tasks to run on a CPU), so that at 
    *    any time we can get the next element (according to a certain criterion), remove it from 
    *    the list, and (usually) stop worrying about fixing anything for the other elements. The 
    *    difference with a sorted list is that we only go through elements in a priority queue once,
    *    and the elements already removed from the list won’t matter for the ordering anymore.
    *
    *  - La Rocca
    */
    public class PriorityQueue<T> : IPriorityQueue<T>
    where T : IEquatable<T>
    {
        private PriorityQueueOptions options = new()
        {
            // defaults
            HeapType = HeapType.MaxHeap,
            MaxLeafCount = 4
        };

        readonly List<PriorityQueueNode<T>> dataSet = new();

        public PriorityQueue(IEnumerable<(T, int)> pairs, PriorityQueueOptions? opts = null)
        {
            options = opts ?? options;

            dataSet = pairs
              .Select((x) => new PriorityQueueNode<T>(element: x.Item1, priority: x.Item2))
              .ToList();
            Heapify();
        }

        public PriorityQueue(PriorityQueueOptions? opts = null)
        {
            options = opts ?? options;
        }

        public int Count => this.dataSet.Count;
        public void Insert(T element, int priority)
        {
            dataSet.Add(new PriorityQueueNode<T>(element, priority));
            BubbleUp();
        }

        public T Peek()
        {
            return dataSet[0].Element;
        }

        public void Remove(T element)
        {
            int listCount = dataSet.Count;
            if (listCount == 0)
                throw new ApplicationException("Cannot Remove from empty PriorityQueue");

            for (int i = 0; i < listCount; i++)
            {
                var node = dataSet[i];
                if (!node.Element.Equals(element)) continue;

                dataSet[i] = dataSet[listCount - 1];
                dataSet.RemoveAt(listCount - 1);
                PushDown(i);
                break;
            }
        }

        public T Top()
        {
            if (dataSet.Count == 0)
                throw new ApplicationException("Cannot Top empty PriorityQueue");

            T top = this.dataSet[0].Element;
            int lastIndex = dataSet.Count - 1;
            dataSet[0] = dataSet[lastIndex];
            dataSet.RemoveAt(lastIndex);
            PushDown();
            return top;
        }

        public void Update(T element, int newPriority)
        {
            if (dataSet.Count == 0)
                throw new ApplicationException("Cannot Update empty PriorityQueue");

            for (int i = 0; i < dataSet.Count; i++)
            {
                var node = dataSet[i];
                if (!node.Element.Equals(element)) continue;

                if (node.Priority != newPriority)
                {
                    dataSet[i] = new PriorityQueueNode<T>(node.Element, newPriority);
                    if (IsOnTop(newPriority, node.Priority)) BubbleUp(i);
                    else PushDown(i);
                }
                break;
            }
        }

        public void Clear()
        {
            this.dataSet.Clear();
        }

        void BubbleUp(int index = -1)
        {
            if (index < 0) index = this.dataSet.Count - 1;
            PriorityQueueNode<T> bubble = this.dataSet[index];
            int leafCount = options.MaxLeafCount;

            while (index > 0)
            {
                int parentIndex = (int)Math.Floor((double)((index - 1) / leafCount));

                if (IsOnTop(bubble.Priority, dataSet[parentIndex].Priority))
                {
                    dataSet[index] = dataSet[parentIndex];
                    index = parentIndex;
                }
                // parent has a greater priority; time to end
                else break;
            }
            dataSet[index] = bubble;
        }


        void PushDown(int index = 0)
        {
            if (this.dataSet.Count == 0) return;

            PriorityQueueNode<T> stone = dataSet[index];
            int lastIndex = this.dataSet.Count - 1;
            int lastLeafyBranchIndex = GetParentIndex(lastIndex);
            int leafCount = options.MaxLeafCount;
            while (index <= lastLeafyBranchIndex)
            {
                int highestPriority = stone.Priority;
                int hightstPriorityIndex = index;

                // this for loop makes adding maxLeafCount expensive
                for (int x = 1; x <= leafCount; x++)
                {
                    int childIndex = (leafCount * index) + x;
                    if (childIndex > lastIndex) continue;

                    int childPriority = dataSet[childIndex].Priority;
                    if (IsOnTop(childPriority, highestPriority))
                    {
                        highestPriority = childPriority;
                        hightstPriorityIndex = childIndex;
                    }
                }

                if (highestPriority == stone.Priority) break;
                else
                {
                    dataSet[index] = dataSet[hightstPriorityIndex];
                    index = hightstPriorityIndex;
                }
            }

            this.dataSet[index] = stone;
        }

        void Heapify(int index = -1)
        {
            if (index < 0) index = GetParentIndex(dataSet.Count - 1);
            for (; index >= 0; index--)
            {
                PushDown(index);
            }
        }

        int GetParentIndex(int childIndex)
          => (childIndex - (((childIndex - 1) % options.MaxLeafCount) + 1)) / options.MaxLeafCount;

        bool IsOnTop(int a, int b)
        {
            if (a == b) return false;
            bool isMaxHeap = options.HeapType.IsMax();
            if (a > b) return isMaxHeap;
            return !isMaxHeap;
        }
    }



    internal class PriorityQueueNode<T>
    {
        public PriorityQueueNode(T element, int priority)
        {
            Element = element;
            Priority = priority;
        }
        public T Element { get; set; }
        public int Priority { get; set; }
    }

    public struct PriorityQueueOptions
    {
        public HeapType HeapType { get; set; }
        public int MaxLeafCount { get; set; }
    }


}