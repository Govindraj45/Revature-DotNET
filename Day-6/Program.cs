using System;

namespace Day5
{
    class Program
    {
        public static void Main()
        {
            // We make two objects.
            var res1 = new Resource("Res1"); // first object
            var res2 = new Resource("Res2"); // second object

            // We drop the first object, so GC can clean it.
            res1 = null;
            // We keep the second object, so GC keeps it.

            // We ask GC to clean now (just for demo).
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("GC completed");
        }
    }
}
