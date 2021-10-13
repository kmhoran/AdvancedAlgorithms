# Treaps

A hybrid between a tree and a heap. A treap uses heap properties to build a more balanced tree.

Gives us the ability to efficiently store and access data given multiple search criterion. 
These criterion are measurable and sortable in some way.

## Two-Dimensional Criterea

```
priority (numeric heap)
^
|         (d,1)
|         /   \
|     (b,2)   (e,3)
|     /   \       \
| (a,4)   (c,5)   (f,6)
  --------------------> sorted (alpha BST)
```

## Theory

## Binary Search Tree

Provide offer the vest agerage perfomance across standard operations like `insert`, `remove`, 
  `search`, `min`, and `max`.

## Heap

As we know, heaps give us a great way to keep track of priority. It also happens to be a tree.


## Data Structire

### API
* Top() => element
* Peek() => element
* Insert(element, priority)
* Remove(element)
* Update(element, priority)
* Contains(element) => bool
* Min() => element
* Max() => element
* _rightRotate(parent)
* _leftRotate(parent)


### Rotation

Rotation is a common tree opperation whereby the parent-child relationship is switched.
  - **Right Rotation** (clockwise) is performed on *left* children
  - **Left Rotation** is performed on *right* children

In practice, this means replacing the sub-tree at the parent node with the smaller child tree, and 
  then reinserting the removed branch

Rotation results will by their nature always respect the invariants of a BST. However, it may not
  respect the invariants of the heap. A rotation applied to a valid treap would break the heap
  priority constraints.


## The Problems with Arrays

- Arrays work well when we only need to swap random elements one at a time (like in heap bubbleUp)
- Swapping out whole branches is much less straight forward and can reach O(n) time complexity
- Arrays as used in heaps allow us to maintain ever-complete, left-aligned "trees", but these trees
    do not maintain strict element ordering as a BST does.

### Pointers to the rescue

- Rotations in pointer based trees are trivial O(1)