using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesDemo
{
    public class LinqDemo
    {
        public void Run()
        {
            var students = new List<Student>
            {
                new Student { Name = "Alice", Age = 20, Grade = "A" },
                new Student { Name = "Bob", Age = 22, Grade = "B" },
                new Student { Name = "Charlie", Age = 21, Grade = "A" },
                new Student { Name = "David", Age = 23, Grade = "C" },
                new Student { Name = "Eve", Age = 20, Grade = "B" }
            };

            // var olderStudents = from s in students
            //                     where s.Age > 21
            //                     select new { s.Name };

            // var olderStudents = students.Where(filter);
            // foreach (var s in students) { }

            var olderStudents =
                students
                    .Where(s => s.Age > 21)
                    .Select(s => new { s.Name });

            foreach (var student in olderStudents)
            {
                Console.WriteLine($"{student.Name} more than 21 years old.");
            }
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
    }
}
