using System;

class Program{
    static void Main(string[] args){
        // Step 1: Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        double gradePercentage = Convert.ToDouble(Console.ReadLine());

        // Step 2: Determine the letter grade using if-elif-else statements
        char letter;

        if (gradePercentage >= 90){
            letter = 'A';
        }
        else if (gradePercentage >= 80){
            letter = 'B';
        }
        else if (gradePercentage >= 70){
            letter = 'C';
        }
        else if (gradePercentage >= 60){
            letter = 'D';
        }
        else{
            letter = 'F';
        }

        // Step 3: Display the letter grade
        Console.WriteLine($"Your letter grade is: {letter}");

        // Step 4: Check if the user passed the course and display a message
        if (gradePercentage >= 70){
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else{
            Console.WriteLine("Keep it up! You can improve next time.");
        }
    }
}

//Code by Nicolas Velasquez