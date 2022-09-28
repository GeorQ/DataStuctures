using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LockFreeStack 
{
    class LFStack <T>
    {
        private class Node
        {
            public Node Next;
            public T Item;
        }

        private Node head;
        public int capacity;
        public int size;

        public LFStack(int capacity)
        {
            head = new Node();
            this.capacity = capacity;
            size = 0;
        }

        public void Push(T item)
        {
            if (size >= capacity)
            {
                throw new Exception("Exceed stack capacity!");
            }
            Node node = new Node();
            node.Item = item;
            do
            {
                node.Next = head.Next;
            } while (!CompareAndSwap(ref head.Next, node.Next, node));
            size++;
        }

        public bool Pop(out T result)
        {
            if (IsEmpty())
            {
                throw new Exception("There are no elements in stack!");
            }
            Node node;
            do
            {
                node = head.Next;
                if (node == null)
                {
                    result = default(T);
                    return false;
                }
            } while (!CompareAndSwap(ref head.Next, node, node.Next));
            result = node.Item;
            size--;
            return true;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Clear()
        {
            head = new Node();
            size = 0;
        }

        // if headNext is nodeNext 
        // (that was assigned now, that means that no other thread has inserted or removed something,
        // if some thead had assigned something then head.next would not have the same value as node.next) 
        // then node.next is assigned to head.next
        private static bool CompareAndSwapUnsafe(ref Node destination, Node currentValue, Node newValue)
        {
            // has to be atomar
            if (destination == currentValue)
            {
                destination = newValue;
                return true;
            }
            return false;
        }

       
        // CompareExchange compares one value with another value and if they are the // same a new value is asigned to the // first value.
        // This method insure that operation would be atomic// that means that there is no context switch possible.
        private static bool CompareAndSwap(ref Node destination, Node currentValue, Node newValue)
        {
            if (currentValue == Interlocked.CompareExchange<Node>(ref destination, newValue, currentValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
