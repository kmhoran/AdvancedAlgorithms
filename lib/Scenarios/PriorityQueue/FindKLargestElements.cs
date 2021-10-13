using System;
using System.Linq;
using Structures;
using Structures.Enums;

namespace Scenarios.PriorityQueue
{
    /**
    * In this section we are going to describe how we can use a priority queue to keep track of the
    *   k largest elements of a set.
    *
    * If we have the full set of n elements in advance, we have a few alternatives that don’t need 
    *  any auxiliary data structure:
    *
    *     - We could sort the input and take the last k elements. This naïve approach requires 
    *         O(n*log(n)) comparisons and swaps and, depending on the algo-rithm, might require 
    *         additional memory.
    *
    *     - We could find the largest element from the set and move it to the end of the array,
    *         then look at the remaining n-1 elements and find the second to last and move it to 
    *         position n-2 , and so on. Basically, this algorithm runs the inner cycle of Selection 
    *         Sort algorithm k times, requiring O(k) swaps and O(n*k) com-parisons. No additional 
    *         memory would be needed.
    *
    * In this section we will see that by using a heap, we can achieve our goal using O(n+k*log(k)) 
    *   comparisons and swaps, and O(k) extra memory. This is a game-changing improvement if k is 
    *   much smaller than n . In a typical situation, n could be on the order of millions or 
    *   billions of elements, and k between a hundred and a few thousand.
    *
    * Moreover, by using an auxiliary heap, the algorithm can naturally be adapted to work on 
    *   dynamic streams of data and also to allow consuming elements from the heap.
    *
    * - La Rocca
    */

    /**
    * Rather than bringing the whole set into memory, we limit ourselves to only holding k elements
    *
    * If we use a MIN-heap, we can compare each new element against our top-k heap. If it's less than the top value, we continue on
    */
    public static class FindKLargestElements
    {
        public static void Go(int k, int n)
        {
            var dataSet = GetData(n);
            var topResults = new PriorityQueue<int>(
                dataSet.Take(k).Select(x => (x, x)),
                new PriorityQueueOptions
                {
                    HeapType = HeapType.MinHeap,
                    MaxLeafCount = 3
                }
            );

            for (int i = k; i < n; i++)
            {
                int fromData = dataSet[i];
                if (fromData <= topResults.Peek()) continue;

                // add higer priority element
                topResults.Insert(element: fromData, priority: fromData);

                // pop the lowest value
                topResults.Top();
            }

            Console.WriteLine($"Found top {k} items in a list of {n}:");
            while (topResults.Count > 0)
            {
                Console.WriteLine($"    -> {topResults.Top()}");
            }

        }

        static int[] GetData(int n)
        {
            int minValue = 0;
            int maxValue = 1000;

            int[] result = new int[n];

            Random rando = new();

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = rando.Next(minValue, maxValue);
            }
            return result;
        }
    }
}
