# Heaps

## Priority Queue
It's important in requirements gathering to determine if we need to store the entire list by ordered
  priority.

If not, and we only need to pull the next highest priority item, a heap would be a more efficient 
  and maintainable option.

Heaps keep a parial ordering of the elements, while guaranteeing that the next available option is 
  the highest-priority element.

Heaps are generally the structure of choice for priority queues.

## Data Structure

### API
* Top() => element
* Peek() => element
* Insert(element, priority)
* Remove(element)
* Update(element, priority)
* _bubbleUp(list, index)
* _pushDown(list, index)
* _heapify()

### Contract With Client
The top element returned by the queue is always the element with the highest priority currently in 
  the queue

### Data Model
Array whose elements are items stored in the heap

### Invariants
- Every element has two "children." For the element at position *i*, its children are located at 
  indicies `2*i+1` and `2*i+2`
- Every element has higher priority than its children

## Min-heap vs Max-heap
- neither is better, but the book will use a max-heap
- an easy trick to convert between the two is to multiply priorities by `-1`!

## d-ary heaps
There is no rule that says we need limit ourself to a binary tree.

Where `d` is the leaf count, we can determine the indecies of the child `x` (x = 1 , ..., d)of 
  element `i` as:
`d*i + x`

So that if `d` = 3, the **3** children of `i` could be found at (`3*i+1, 3*i+2, 3*i+3`)

## BubbleUp()

When data is added, removed, or updated for a heap, a bubble-up function must be called to ensure 
  the data structure respects its invariants.


## PushDown()

* When a parent may suddenly be lower priority than its child...
* or when the root node is removed and replaced with the last index


## Heapify()

Heapifying a complete set is O(n), while mapping with insert will land us O(n*log(n))