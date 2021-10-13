using System;

using System.Collections.Generic;
using System.Linq;
using Structures.Enums;
using Structures.Interfaces;

namespace Structures
{
    /**
    *  Sorted Priority Queue implemented as a treap
    */
    public class SortedPriorityQueue<T> : ISortedPriorityQueue<T>
    where T : IEquatable<T>, IComparable<T>
    {
        SortedPriorityQueueNode<T> root;
        int _count = 0;

        public SortedPriorityQueue()
        {

        }

        public int Count => _count;

        public void Clear()
        {
            this.root = null;
            this._count = 0;
        }

        public bool Contains(T element)
        {
            return Search(element) != null;
        }

        public void Insert(T element, double priority)
        {
            this._count += 1;
            SortedPriorityQueueNode<T> newNode = new(element, priority);
            if (this.root == null)
            {
                this.root = newNode;
                return;
            }

            SortedPriorityQueueNode<T> parent = null;
            bool isLeftChild = false;
            var currentNode = this.root;

            // place at bottom of tree
            while (currentNode != null)
            {
                isLeftChild = currentNode.Value.CompareTo(element) > 0;
                parent = currentNode;
                if (isLeftChild) currentNode = currentNode.Left;
                else currentNode = currentNode.Right;
            }

            if (parent != null)
            {
                if (isLeftChild) parent.SetLeft(newNode);
                else parent.SetRight(newNode);
            }

            // swing up the tree
            while (newNode.Parent != null && IsTopPriority(newNode, newNode.Parent))
            {
                bool isRightChild = newNode.Parent.Right != null
                  && newNode.Parent.Right.Equals(newNode);

                if (isRightChild) LeftRotateRightChild(newNode); // left rotate the right child
                else RightRotateLeftChild(newNode); // right rotate the left child
            }
            if (newNode.Parent == null) this.root = newNode;
        }

        public T Max()
        {
            if (this.root == null)
                throw new ApplicationException("Cannot find max of empty queue.");

            var currentNode = this.root;

            while (currentNode.Right != null)
            {
                currentNode = currentNode.Right;
            }

            return currentNode.Value;
        }

        public T Min()
        {
            if (this.root == null)
                throw new ApplicationException("Cannot find min of empty queue.");

            var currentNode = this.root;

            while (currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }

            return currentNode.Value;
        }

        public T Peek()
        {
            if (this.root == null)
                throw new ApplicationException("Cannot peek empty queue.");

            return this.root.Value;
        }

        public void Remove(T element)
        {
            if (this.root == null)
                throw new ApplicationException("Cannot remove from empty queue.");
            var toRemove = Search(element);
            if (toRemove == null) return;

            this._count -= 1;

            // push the removal node to a leaf & remove
            while (!IsLeafNode(toRemove))
            {
                bool isRoot = toRemove.Equals(this.root);
                // Rotate with top-priority node
                // [MIN-HEAP] low priorities bubble up
                bool isRightRotation = toRemove.Left != null
                    && (toRemove.Right == null || toRemove.Right.Priority > toRemove.Left.Priority);

                if (isRightRotation) RightRotateLeftChild(toRemove.Left);
                else LeftRotateRightChild(toRemove.Right);
                if (isRoot) this.root = toRemove.Parent;
            }
            if (toRemove.Parent != null)
            {
                bool isLeftChild = toRemove.Parent.Value.CompareTo(toRemove.Value) > 0;
                if (isLeftChild) toRemove.Parent.SetLeft(null);
                else toRemove.Parent.SetRight(null);
            }
            else this.root = null;
        }

        public T Top()
        {
            if (this.root == null)
                throw new ApplicationException("Cannot get top of empty queue.");
            var result = this.root.Value;
            Remove(result);
            return result;
        }

        public void Update(T element, double newPriority)
        {
            if (this.root == null)
                throw new ApplicationException("Cannot update empty queue.");
            var toUpdate = Search(element);
            if (toUpdate == null) return;

            toUpdate.Priority = newPriority;

            // buddle up if needed
            while (toUpdate.Parent != null && IsTopPriority(toUpdate, toUpdate.Parent))
            {
                bool isLeftChild = toUpdate.Parent.Value.CompareTo(toUpdate.Value) > 0;
                if (isLeftChild) RightRotateLeftChild(toUpdate);
                else LeftRotateRightChild(toUpdate);
            }

            bool nodeNeedsPushingDown(SortedPriorityQueueNode<T> node) =>
                (node.Right != null && IsTopPriority(node.Right, node))
                || (node.Left != null && IsTopPriority(node.Left, node));

            // function assumes *some* kind of rotation needs to be made
            bool needsRightRotation(SortedPriorityQueueNode<T> node) =>
                node.Left != null
                && (node.Right == null || IsTopPriority(node.Left, node.Right));

            // push down if needed
            while (!IsLeafNode(toUpdate) && nodeNeedsPushingDown(toUpdate))
            {
                if (needsRightRotation(toUpdate)) RightRotateLeftChild(toUpdate.Left);
                else LeftRotateRightChild(toUpdate.Right);
            }
        }


        SortedPriorityQueueNode<T> Search(T element)
        {
            // BST search
            var currentNode = this.root;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(element)) return currentNode;
                var elementIsSmaller = currentNode.Value.CompareTo(element) > 0;
                if (elementIsSmaller) currentNode = currentNode.Left;
                else currentNode = currentNode.Right;
            }

            return null;
        }

        void RightRotateLeftChild(SortedPriorityQueueNode<T> childNode)
        {
            if (childNode == null || childNode.Parent == null)
                throw new ApplicationException("Cannot roatate null or root node");
            var nodeParent = childNode.Parent;
            if (childNode.Equals(nodeParent.Right))
                throw new ApplicationException(
                    "Cannot perform right rotation when node is right child");
            var grandParent = nodeParent.Parent;

            if (grandParent != null)
            {
                if (grandParent.Left == nodeParent)
                    grandParent.SetLeft(childNode);
                else grandParent.SetRight(childNode);
            }
            else
            {
                childNode.Parent = null;
                this.root = childNode;
            }

            nodeParent.SetLeft(childNode.Right);
            childNode.SetRight(nodeParent);
        }

        void LeftRotateRightChild(SortedPriorityQueueNode<T> childNode)
        {
            if (childNode == null || childNode.Parent == null)
                throw new ApplicationException("Cannot rotate null or root node");
            var nodeParent = childNode.Parent;
            if (childNode.Equals(nodeParent.Left))
                throw new ApplicationException(
                    "Cannot perform left rotation when node is left child");
            var grandParent = nodeParent.Parent;

            if (grandParent != null)
            {
                if (grandParent.Right == nodeParent)
                    grandParent.SetRight(childNode);
                else grandParent.SetLeft(childNode);
            }
            else
            {
                childNode.Parent = null;
                this.root = childNode;
            }

            nodeParent.SetRight(childNode.Left);
            childNode.SetLeft(nodeParent);
        }

        static bool IsTopPriority(SortedPriorityQueueNode<T> a, SortedPriorityQueueNode<T> b)
        {
            return a.Priority < b.Priority;
        }
        static bool IsLeafNode(SortedPriorityQueueNode<T> node)
        {
            return node.Left == null && node.Right == null;
        }
    }

    internal class SortedPriorityQueueNode<T>
    {

        public SortedPriorityQueueNode(T value, double priority)
        {
            this.Value = value;
            this.Priority = priority;
        }

        public T Value { get; set; }
        public double Priority { get; set; }
        public SortedPriorityQueueNode<T> Left { get; private set; }
        public SortedPriorityQueueNode<T> Right { get; private set; }
        public SortedPriorityQueueNode<T> Parent { get; set; }

        public void SetLeft(SortedPriorityQueueNode<T> node)
        {
            this.Left = node;
            if (node != null) node.Parent = this;
        }
        public void SetRight(SortedPriorityQueueNode<T> node)
        {
            this.Right = node;
            if (node != null) node.Parent = this;
        }
    }
}