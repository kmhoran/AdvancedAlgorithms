using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Structures;
using Structures.Enums;

namespace Scenarios.PriorityQueue
{
    public static class HuffmanCompression
    {
        public static void Go(string text)
        {
            text = text.ToLower();
            Console.WriteLine($"Huffman Compression: {text}\n");
            Console.WriteLine("CHAR      | SEQUENCE");
            Console.WriteLine("========= | =========");
            var table = Compress(text);

            foreach (var kvp in table.AsEnumerable())
            {
                Console.WriteLine($"{kvp.Key}         | {kvp.Value}");
            }

        }

        static Dictionary<char, int> Compress(string text)
        {
            var textLength = text.Length;
            if (text.Length <= 0) return new Dictionary<char, int>();
            var frequencyMap = GetFrequencyMap(text);
            var priorityQueue = new PriorityQueue<HuffmanNode>(new PriorityQueueOptions() { HeapType = HeapType.MinHeap, MaxLeafCount = 2 });

            foreach (var kvp in frequencyMap)
            {
                int priority = (int)Math.Floor(kvp.Value * textLength);
                priorityQueue.Insert(new HuffmanNode(new char[] { kvp.Key }, kvp.Value), priority);
            }

            while (priorityQueue.Count > 1)
            {
                var left = priorityQueue.Top();
                var right = priorityQueue.Top();
                var parent = new HuffmanNode(SortChars(left.Value.Concat(right.Value).ToArray()), left.Frequency + right.Frequency)
                {
                    Left = left,
                    Right = right
                };
                int priority = (int)Math.Floor(parent.Frequency * textLength);
                priorityQueue.Insert(parent, priority);
            }
            return BuildTable(priorityQueue.Top());
        }

        static Dictionary<char, int> BuildTable(HuffmanNode node, int sequence = 0, Dictionary<char, int> charactersToSequenceMap = null)
        {
            if (charactersToSequenceMap == null) charactersToSequenceMap = new Dictionary<char, int>();
            if (node.Value.Length == 1)
            {
                charactersToSequenceMap[node.Value[0]] = sequence;
            }
            else
            {
                if (node.Left != null) BuildTable(node.Left, 0 + sequence, charactersToSequenceMap);
                if (node.Right != null) BuildTable(node.Right, 1 + sequence, charactersToSequenceMap);
            }

            return charactersToSequenceMap;
        }

        static readonly PriorityQueue<char> charSortQueue = new();

        public static char[] SortChars(char[] chars)
        {
            int charsLength = chars.Length;
            char[] result = new char[charsLength];
            for (int i = 0; i < charsLength; i++)
            {
                char c = chars[i];
                charSortQueue.Insert(c, c);
            }
            for (int i = 0; i < charsLength; i++)
            {
                result[i] = charSortQueue.Top();
            }
            charSortQueue.Clear();
            return result;
        }


        public static Dictionary<char, double> GetFrequencyMap(string text)
        {
            double initialValue = 1 / (double)text.Length;
            Dictionary<char, double> map = new();

            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                if (map.ContainsKey(character)) map[character] = map[character] + initialValue;
                else map[character] = initialValue;
            }
            return map;
        }
    }

    internal class HuffmanNode : IEquatable<HuffmanNode>
    {
        internal HuffmanNode(char[] value, double frequency)
        {
            this.Value = value;
            this.Frequency = frequency;
        }


        internal char[] Value { get; set; }
        internal double Frequency { get; set; }
        internal HuffmanNode Left { get; set; }
        internal HuffmanNode Right { get; set; }

        public bool Equals(HuffmanNode other)
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as HuffmanNode);
        }

        public override int GetHashCode()
        {
            return string.Join(string.Empty, Value).GetHashCode();
        }
    }
}