using System;

namespace DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            PriorityStack<int> ps = new PriorityStack<int>();
            //After each "Push()" and "Clear()" method call, information print method called as well, in order to disable it
            //Call "SetActiveDetails()" with "false" argument

            //ps.SetActiveDetails(false);

            //Uncomment to check for empty stack exception
            //Console.WriteLine(ps.Pop());

            ps.Push(10, 10);
            ps.Push(11, 30);
            ps.Push(10, 20);
            ps.Push(10, 40);
            Console.WriteLine(ps.Pop());
            Console.WriteLine(ps.Pop());
            Console.WriteLine();
            ps.Push(12, 60);
            ps.Push(10, 70);
            ps.Push(10, 1);
            ps.Push(10, 2);
            Console.WriteLine(ps.Pop());
            Console.WriteLine(ps.Pop());
            Console.WriteLine();
            ps.Clear();
            ps.Push(1, 20);
            Console.WriteLine(ps.Pop());
        }
    }
}
