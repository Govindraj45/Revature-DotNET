using System;

namespace Day5
{
    public static class RecordDemo
    {
        public static void Run()
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

    public record Temp
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
