using System;

namespace Day5
{
    public class Resource
    {
        public string Name { get; set; }

        public Resource(string name)
        {
            Name = name;
            // Say hello when object is born.
            Console.WriteLine($"{Name} created");
        }

        // Destructor (Finalizer)
        ~Resource()
        {
            // Say bye when GC removes it.
            Console.WriteLine($"{Name} destroyed by GC");
        }
    }
}
