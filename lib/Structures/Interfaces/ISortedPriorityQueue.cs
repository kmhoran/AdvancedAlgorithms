using System;

namespace Structures.Interfaces
{
    public interface ISortedPriorityQueue<T>
    {
        int Count { get; }
        T Top();
        T Peek();
        void Insert(T element, double priority);
        void Remove(T element);
        void Update(T element, double newPriority);
        void Clear();
        bool Contains(T element);
        T Min();
        T Max();
    }
}
