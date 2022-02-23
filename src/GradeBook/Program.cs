// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example: args and concatenate string
            // static keyword: - It is not associated with any objects, - no need to create instance can directly use Class name to access methods/properties.
            if (args.Length > 0)
            {
                Console.WriteLine($"Hello, {args[0]}!");
            }
            else
            {
                Console.WriteLine("Hello!");
            }

            //=====================================================================================
            // Example: Array use/implement
            var numbers = new[] { 12.7, 10.3, 6.11, 4.1 };
            var resultArray = 0.0;
            foreach (double number in numbers)
            {
                resultArray += number;
            }
            Console.WriteLine(resultArray);

            //=====================================================================================
            // Example: List use/implement -> List is dynamic/ N1: number of digits after precision
            List<double> grades = new List<double>() { 12.7, 10.3, 6.11, 4.1 };
            grades.Add(56.1);
            var resultList = 0.0;
            var highestGrade = double.MinValue;
            var lowestGrade = double.MaxValue;
            foreach (double number in grades)
            {
                lowestGrade = Math.Min(number, lowestGrade);
                highestGrade = Math.Max(number, highestGrade);
                resultList += number;
            }
            resultList = resultList / grades.Count;
            Console.WriteLine($"Main: The average grade is {resultList:N1}");
            Console.WriteLine($"Main: The highest grade is {highestGrade:N1}");
            Console.WriteLine($"Main: The lowest grade is {lowestGrade:N1}");

            //=====================================================================================
            // Example: Class,Interface,etc implementation for business logic separation
            var book = new InMemoryBook("Grade Book");
            // book.AddGrade(89.1);
            // book.AddGrade(90.5);
            // book.AddGrade(77.5);
            book.GradeAdded += OnGradeAdded;
            EnterGrade(book);
            var stats = book.GetStatistics();
            Console.WriteLine($"Class: For the book named {book.Name}");
            Console.WriteLine($"Class: The average grade is {stats.Average:N1}");
            Console.WriteLine($"Class: The highest grade is {stats.High:N1}");
            Console.WriteLine($"Class: The lowest grade is {stats.Low:N1}");
            Console.WriteLine($"Class: The letter grade is {stats.Letter}");

            //====================================================================================
            // Example: Multiple class with same base class, also data store in memory
            Book book2 = new DiskBook("Disk Grade Book");
            book2.GradeAdded += OnGradeAdded;
            EnterGrade(book2);
            var stats1 = book2.GetStatistics();
            Console.WriteLine($"Class(Disk): For the book named {book2.Name}");
            Console.WriteLine($"Class(Disk): The average grade is {stats1.Average:N1}");
            Console.WriteLine($"Class(Disk): The highest grade is {stats1.High:N1}");
            Console.WriteLine($"Class(Disk): The lowest grade is {stats1.Low:N1}");
            Console.WriteLine($"Class(Disk): The letter grade is {stats1.Letter}");
        }

        private static void EnterGrade(IBook book)
        {
            var done = false;
            while (!done)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    done = true;
                    continue;
                }
                try
                {
                    if (input != null)
                    {
                        var grade = double.Parse(input);
                        book.AddGrade(grade);
                    }
                    else
                    {
                        done = false;
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}