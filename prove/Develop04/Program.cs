using System;
using System.Collections.Generic;
using System.Threading;

class Activity{
    protected string _name;
    protected string _description;
    protected int _duration;

    public Activity(string name, string description, int duration){
        _name = name;
        _description = description;
        _duration = duration;
    }

    protected void DisplayStartingMessage(){
        Console.WriteLine($"Welcome to the {_name}!");
        Console.WriteLine($"Duration: {_duration} seconds\n");
        Console.WriteLine(_description);
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3); // Pause before starting
    }

    protected void DisplayEndingMessage(){
        Console.WriteLine($"Great job! You've completed the {_name}.");
        Console.WriteLine($"Total time: {_duration} seconds");
        ShowSpinner(3); // Pause before finishing
    }

    protected void ShowSpinner(int seconds){
        for (int i = 0; i < seconds; i++){
            Thread.Sleep(1000);
            Console.Write(".");
        }
        Console.WriteLine();
    }

    protected void ShowCountDown(int seconds){
        for (int i = seconds; i > 0; i--){
            Console.WriteLine($"{i}");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : Activity{
    public BreathingActivity() : base("Breathing Activity", "Deep breathing increases the supply of oxygen to our brain and stimulates the parasympathetic nervous system, producing relaxation and calm.\nIt diverts your attention from your worries and helps you feel more connected to your inner self.\nThis activity will help you relax by walking your through breathing in and out slowly.\nClear your mind and focus on your breathing.", 60){
    }

    public void Run(){
        DisplayStartingMessage();

        for (int seconds = 0; seconds < _duration; seconds += 3){
            Console.WriteLine("Breathe in...");
            ShowCountDown(3);

            if (seconds + 2 < _duration){
                Console.WriteLine("Breathe out...");
                ShowCountDown(3);
            }
        }

        DisplayEndingMessage();
    }
}

class ListingActivity : Activity{
    private int _count;
    private List<string> _prompts;

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.", 90){
        _count = 0;
        _prompts = new List<string>{
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    public void Run(){
        DisplayStartingMessage();
        string randomPrompt = GetRandomPrompt();
        Console.WriteLine(randomPrompt);

        Console.WriteLine("Get ready to list as many items as you can...");
        ShowCountDown(5); // Pause before listing

        GetListFromUser();

        DisplayEndingMessage();
    }

    private string GetRandomPrompt(){
        return _prompts[new Random().Next(_prompts.Count)];
    }

    private void GetListFromUser(){
        Console.WriteLine("Enter each item on a new line. Type 'done' when finished:");
        string input;
        do{
            input = Console.ReadLine();
            if (input.ToLower() != "done")
                _count++;
        } while (input.ToLower() != "done");

        Console.WriteLine($"You listed {_count} items.");
    }
}

class ReflectingActivity : Activity{
    private List<string> _prompts;
    private List<string> _questions;

    public ReflectingActivity() : base("Reflecting Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.", 120){
        _prompts = new List<string>{
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>{
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
    }

    public void Run(){
        DisplayStartingMessage();
        string randomPrompt = GetRandomPrompt();
        Console.WriteLine(randomPrompt);

        DisplayQuestions();

        DisplayEndingMessage();
    }

    private string GetRandomPrompt(){
        return _prompts[new Random().Next(_prompts.Count)];
    }

    private string GetRandomQuestion(){
        return _questions[new Random().Next(_questions.Count)];
    }

    private void DisplayQuestions(){
        Console.WriteLine("Reflect on the following questions:");
        for (int i = 0; i < _duration; i += 8)
        {
            ShowSpinner(8);
            Console.WriteLine(GetRandomQuestion());
        }
    }
}

class Program{
    static void Main(){
        while (true){
            Console.Clear();
            Console.WriteLine("Mindfulness Activities Menu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflecting Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");

            Console.Write("Enter your choice (1-4): ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int activityChoice)){
                switch (activityChoice){
                    case 1:
                        RunActivity(new BreathingActivity());
                        break;

                    case 2:
                        RunActivity(new ReflectingActivity());
                        break;

                    case 3:
                        RunActivity(new ListingActivity());
                        break;

                    case 4:
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        break;
                }
            }
            else{
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }

    static void RunActivity(BreathingActivity activity){
        activity.Run();
    }

    static void RunActivity(ReflectingActivity activity){
        activity.Run();
    }

    static void RunActivity(ListingActivity activity){
        activity.Run();
    }
}
