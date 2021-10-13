using System;

namespace Structures.Interfaces
{
    public interface IPriorityQueue<T>
    {
        int Count { get; }
        T Top();
        T Peek();
        void Insert(T element, int priority);
        void Remove(T element);
        void Update(T element, int priority);
        void Clear();
    }
}
