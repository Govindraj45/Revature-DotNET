using System;
using System.Collections;

namespace Day6
{
    public static class ArrayCollectionDemo
    {
        public static void Run()
        {
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add("Hello");
            list.Add(3.14);
            foreach (var item in list)
            {
                // Console.WriteLine(item);
            }

            int sum = 0;
            foreach (var item in list)
            {
                Console.WriteLine($"item: {item}, type: {item.GetType()}");
                sum += (int)item;
                Console.WriteLine($"Sum: {sum}");
            }
        }
    }
}
