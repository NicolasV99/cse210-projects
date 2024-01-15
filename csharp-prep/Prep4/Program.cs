using System;
using System.Collections.Generic;

class Program{
    static void Main(string[] args){
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int userInput;

        do
        {
            Console.Write("Enter number: ");
            userInput = Convert.ToInt32(Console.ReadLine());

            if (userInput != 0)
            {
                numbers.Add(userInput);
            }

        } while (userInput != 0);

        // Core Requirement 1: Compute the sum of the numbers
        int sum = numbers.Sum();

        // Core Requirement 2: Compute the average of the numbers
        double average = numbers.Average();

        // Core Requirement 3: Find the maximum number in the list
        int maxNumber = numbers.Max();

        // Display the results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maxNumber}");
    }
}