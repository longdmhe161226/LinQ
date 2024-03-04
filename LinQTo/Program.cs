using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3,3, 4, 5, 6, 7, 8, 9, 10 };

        // Partitioning
        var firstThreeNumbers = numbers.Take(3);
        Console.WriteLine("First Three Numbers: " + string.Join(", ", firstThreeNumbers));

        // Ordering
        var descendingOrder = numbers.OrderByDescending(n => n);
        Console.WriteLine("Descending Order: " + string.Join(", ", descendingOrder));


        // Set
        List<int> anotherNumbers = new List<int> { 5, 6, 7, 8, 9, 10 };
        var distinctNumbers = numbers.Distinct();
        Console.WriteLine("Distinct Numbers: " + string.Join(", ", distinctNumbers));

        var unionNumbers = numbers.Union(anotherNumbers);
        Console.WriteLine("Union Numbers: " + string.Join(", ", unionNumbers));

        // Element
        int firstNumber = numbers.First();
        Console.WriteLine("First Number: " + firstNumber);

        int lastNumber = numbers.Last();
        Console.WriteLine("Last Number: " + lastNumber);

        int elementAtPositionThree = numbers.ElementAt(2);
        Console.WriteLine("Element at Position Three: " + elementAtPositionThree);

        // Generation
        var rangeNumbers = Enumerable.Range(4,6);
        Console.WriteLine("Range Numbers: " + string.Join(", ", rangeNumbers));

        var repeatedNumbers = Enumerable.Repeat(11, 4);
        Console.WriteLine("Repeated Numbers: " + string.Join(", ", repeatedNumbers));

        // Quantifier
        bool anyEvenNumbers = numbers.Any(n => n % 2 == 0);
        Console.WriteLine("Any Even Numbers: " + anyEvenNumbers);

        bool allEvenNumbers = numbers.All(n => n % 2 == 0);
        Console.WriteLine("All Even Numbers: " + allEvenNumbers);

        // Aggregate
        int sumOfNumbers = numbers.Sum();
        Console.WriteLine("Sum of Numbers: " + sumOfNumbers);

        int productOfNumbers = numbers.Aggregate((acc, n) => acc * n);
        Console.WriteLine("Product of Numbers: " + productOfNumbers);

    }
}
