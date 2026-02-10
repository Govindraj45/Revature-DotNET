using System;
using System.Collections.Generic;

namespace Day6
{
    public static class CollectionClassDemo
    {
        public static void CollectionClassesDemo()
        {
            List<string> list = new List<string>();

            list.Add("1");
            list.Add("2");
            list.Add("3");
            list.Add("4");

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
