using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructure
{
    class PriorityStack<T>
    {
        public struct ArrayElement
        {
            public int Priority { get; private set; }
            public T Value { get; private set; }
            public ArrayElement(int p, T v)
            {
                Priority = p;
                Value = v;
            }
        }

        private ArrayElement[] stack;
        private const int initialSize = 10;
        private const int resizeCoefficient = 2;
        private int currentSize;
        //When array is fiiled on this procent it has to be resized
        private const float procentToResize = 60f;
        private int currentIndex;
        private bool isShowDetails = true;

        public PriorityStack()
        {
            stack = new ArrayElement[initialSize];
            currentSize = initialSize;
            currentIndex = 0;
        }

        // Add element to the stack
        public void Push(int priority, T value)
        {
            //Check wether array should be resized or not before pushing an element
            Resize();
            ArrayElement item = new ArrayElement(priority, value);
            for (int i = 0; i <= currentIndex; i++)
            {
                if (priority >= stack[i].Priority || i == currentIndex)
                {
                    Insert(i, item);
                    break;
                }
            }
            PrintDetails();
        }

        // Take element with highest priority, in case of same prior take last in
        public T Pop()
        {
            if (IsEmpty())
            {
                throw new Exception("There are no elements in the stack!");
            }
            T result = stack[0].Value;
            //Shift left
            for (int i = 0; i < currentIndex - 1; i++)
            {
                stack[i] = stack[i + 1];
            }
            currentIndex -= 1;
            return result;
        }

        // Check wether stack is empty or not
        public bool IsEmpty()
        {
            return currentIndex == 0;
        }

        // Clear the stack
        public void Clear()
        {
            stack = new ArrayElement[initialSize];
            currentIndex = 0;
            currentSize = initialSize;
            PrintDetails();
        }
        
        private void Resize()
        {
            if ((float)currentIndex / (float)stack.Length * 100 >= procentToResize)
            {
                currentSize *= resizeCoefficient;
                ArrayElement[] temp = new ArrayElement[currentSize];
                for (int i = 0; i < currentIndex; i++)
                {
                    temp[i] = stack[i];
                }
                stack = temp;
            }
        }

        //Insert element in array at specific index 
        private void Insert(int index, ArrayElement element)
        {
            //Shift right from index 
            for (int i = currentIndex; i > index; i--)
            {
                stack[i] = stack[i - 1];
            }
            stack[index] = element;
            currentIndex++;
        }

        public void SetActiveDetails(bool isActive)
        {
            isShowDetails = isActive;
        }

        //Print info abut stack
        private void PrintDetails()
        {
            if (!isShowDetails)
            {
                return;
            }
            Console.WriteLine($"Current size - {currentSize}, Current index - {currentIndex}, stack is filled on " +
                $"{(float)currentIndex / (float)stack.Length * 100}%");
            for (int i = 0; i < currentIndex; i++)
            {
                Console.WriteLine($"Order: {i}, Prior: {stack[i].Priority}, Value: {stack[i].Value}");
            }
            Console.WriteLine();
        }
    }
}
