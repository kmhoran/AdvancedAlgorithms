using System;

namespace Structures.Enums
{
    public enum HeapType
    {
        MaxHeap,
        MinHeap
    }

    public static class HeapTypeExtentions
    {
        public static bool IsMax(this HeapType type) => type == HeapType.MaxHeap;
        public static bool IsMin(this HeapType type) => type == HeapType.MinHeap;
    }
}
