using System;
using System.Collections.Generic;

namespace Day5
{
    public static class ListCapacityDemo
    {
        public static void Run()
        {
            List<int> marks = new List<int>(10);

            marks.Add(1);
            marks.Add(1);
            Console.WriteLine($"Count: {marks.Count}, Capacity: {marks.Capacity}");

            marks.AddRange(new int[] { 1, 2, 3 });
            Console.WriteLine($"Count: {marks.Count}, Capacity: {marks.Capacity}");

            marks.AddRange(new List<int> { 4, 5, 6 });
            Console.WriteLine($"Count: {marks.Count}, Capacity: {marks.Capacity}");

            marks.AddRange(new List<int> { 4, 5, 6 });
            Console.WriteLine($"Count: {marks.Count}, Capacity: {marks.Capacity}");
        }
    }
}
