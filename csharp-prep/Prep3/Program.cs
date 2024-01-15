using System;

class Program{
    static void Main(string[] args){
        // Step 1: Ask the user for the magic number
        Console.Write("What is the magic number? ");
        int magicNumber = Convert.ToInt32(Console.ReadLine());

        // Step 2: Ask the user for a guess
        int userGuess;

        do{
            Console.Write("What is your guess? ");
            userGuess = Convert.ToInt32(Console.ReadLine());

            // Step 3: Determine if the user needs to guess higher, lower, or if they guessed it
            if (userGuess < magicNumber){
                Console.WriteLine("Higher");
            }
            else if (userGuess > magicNumber){
                Console.WriteLine("Lower");
            }
            else{
                Console.WriteLine("You guessed it!");
            }
        } while (userGuess != magicNumber);

        Console.WriteLine("Game Over!");
    }
}

//Code by Nicolas Velasquez