using System;
using System.Threading;

namespace LockFreeStack
{
    class Program
    {
        static LFStack<int> lfStack;
        static void Main(string[] args)
        {
            lfStack = new LFStack<int>(100);

            //fill with dummy elements, the main problem is to pop not push
            for (int i = 0; i < 100; i++)
            {
                lfStack.Push(i);
            }
            Thread firstThread = new Thread(FirstThreadPop);
            Thread secondThread = new Thread(SecondThreadPop);
            firstThread.Start();
            secondThread.Start();
        }

        public static void FirstThreadPop()
        {
            int result;
            bool b;
            for (int i = 0; i < 5; i++)
            {
                b = lfStack.Pop(out result);
                Console.WriteLine($"First thread took: {result} and bool {b}");
                Thread.Sleep(100);
            }
        }
        public static void SecondThreadPop()
        {
            int result;
            bool b;
            for (int i = 0; i < 5; i++)
            {
                b = lfStack.Pop(out result);
                Console.WriteLine($"Second thread took: {result} and bool {b}");
                Thread.Sleep(100);
            }
        }
    }
}
