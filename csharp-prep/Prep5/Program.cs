using System;

class Program{
    static void Main(string[] args){
        {
            // Call DisplayWelcome function
            DisplayWelcome();

            // Call PromptUserName function
            string userName = PromptUserName();

            // Call PromptUserNumber function
            int userNumber = PromptUserNumber();

            // Call SquareNumber function
            int squaredNumber = SquareNumber(userNumber);

            // Call DisplayResult function
            DisplayResult(userName, squaredNumber);
        }

        // Function to display welcome message
        static void DisplayWelcome(){
            Console.WriteLine("Welcome to the program!");
        }

        // Function to prompt user for name and return it
        static string PromptUserName(){
            Console.Write("Please enter your name: ");
            return Console.ReadLine();
        }

        // Function to prompt user for favorite number and return it
        static int PromptUserNumber(){
            Console.Write("Please enter your favorite number: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        // Function to square a given number and return the result
        static int SquareNumber(int number){
            return number * number;
        }

        // Function to display the result using user's name and squared number
        static void DisplayResult(string userName, int squaredNumber){
            Console.WriteLine($"{userName}, the square of your number is {squaredNumber}");
        }
    }
}

//Code by Nicolas Velasquez