using System;

namespace Day6
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

            Console.WriteLine();
            RecordDemo();

            Console.WriteLine();
            // ArrayCollectionDemo.Run();

            Console.WriteLine();
            ListCapacityDemo.Run();
        }
    
        private static void RecordDemo()
        {
            var temp1 = new Temp { Id = 1, Name = "Temp1" };
            var temp2 = new Temp { Id = 1, Name = "Temp1" };

            Console.WriteLine(temp1);
            Console.WriteLine(temp2);
            Console.WriteLine(temp1 == temp2);

            var temp3 = temp1 with { Id = 2 };
            Console.WriteLine(temp3);
        }
    }

}
