using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal{
    public string _shortName;
    public string _description;
    public int _points;

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}

class SimpleGoal : Goal{
    private bool _isComplete;

    public override void RecordEvent(){
        _isComplete = true;
    }

    public override bool IsComplete(){
        return _isComplete;
    }

    public override string GetStringRepresentation(){
        return _isComplete ? "[X]" : "[ ]";
    }

    public override string GetDetailsString(){
        return $"{_shortName}: {_description} - {_points} points";
    }
}

class EternalGoal : Goal{
    public override void RecordEvent(){
        // Eternal goals are never complete
    }

    public override bool IsComplete(){
        return false;
    }

    public override string GetStringRepresentation(){
        return "[ ]";
    }

    public override string GetDetailsString(){
        return $"{_shortName}: {_description} - {_points} points per event";
    }
}

class ChecklistGoal : Goal{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public override void RecordEvent(){
        _amountCompleted++;

        if (_amountCompleted == _target){
            _points += _bonus;
        }
        else{
            _points += _amountCompleted;
        }
    }

    public override bool IsComplete(){
        return _amountCompleted == _target;
    }

    public override string GetStringRepresentation(){
        return $"Completed {_amountCompleted}/{_target} times";
    }

    public override string GetDetailsString(){
        return $"{_shortName}: {_description} - {_points} points per event, {_bonus} bonus for {_target} completions";
    }
}

class GoalManager{
    private List<Goal> _goals;
    private int _score;

    public GoalManager(){
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start(){
        while (true){
            Console.Clear();
            Console.WriteLine("Eternal Quest Program Menu:");
            Console.WriteLine("1. Display Player Info");
            Console.WriteLine("2. List Goal Names");
            Console.WriteLine("3. List Goal Details");
            Console.WriteLine("4. Create Goal");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Save Goals");
            Console.WriteLine("7. Load Goals");
            Console.WriteLine("8. Quit");

            Console.Write("Enter your choice (1-8): ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int menuChoice)){
                switch (menuChoice){
                    case 1:
                        DisplayPlayerInfo();
                        break;

                    case 2:
                        ListGoalNames();
                        break;

                    case 3:
                        ListGoalDetails();
                        break;

                    case 4:
                        CreateGoal();
                        break;

                    case 5:
                        RecordEvent();
                        break;

                    case 6:
                        SaveGoals();
                        break;

                    case 7:
                        LoadGoals();
                        break;

                    case 8:
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
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

    public void DisplayPlayerInfo(){
        Console.WriteLine($"Current Score: {_score} points");
    }

    public void ListGoalNames(){
        foreach (Goal goal in _goals){
            Console.WriteLine(goal._shortName);
        }
    }

    public void ListGoalDetails(){
        foreach (Goal goal in _goals){
            Console.WriteLine($"{goal._shortName} {goal.GetDetailsString()} {goal.GetStringRepresentation()}");
        }
    }

    public void CreateGoal(){
        Console.Write("Enter goal type (Simple/Eternal/Checklist): ");
        string goalType = Console.ReadLine();

        Goal goal;

        switch (goalType.ToLower()){
            case "simple":
                goal = new SimpleGoal();
                break;

            case "eternal":
                goal = new EternalGoal();
                break;

            case "checklist":
                goal = new ChecklistGoal();
                break;

            default:
                Console.WriteLine("Invalid goal type. Please enter Simple, Eternal, or Checklist.");
                return;
        }

        Console.Write("Enter goal short name: ");
        goal._shortName = Console.ReadLine();

        Console.Write("Enter goal description: ");
        goal._description = Console.ReadLine();

        Console.Write("Enter goal points: ");
        if (int.TryParse(Console.ReadLine(), out int points)){
            goal._points = points;
        }
        else{
            Console.WriteLine("Invalid points. Please enter a valid number.");
            return;
        }

        _goals.Add(goal);
    }

    public void RecordEvent(){
        Console.Write("Enter the index of the goal you completed: ");
        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex >= 0 && goalIndex < _goals.Count){
            Goal goal = _goals[goalIndex];
            goal.RecordEvent();
            _score += goal._points;

            Console.WriteLine($"Event recorded for goal: {goal._shortName}");
            Console.WriteLine($"You earned {goal._points} points");
        }
        else{
            Console.WriteLine("Invalid goal index. Please enter a valid index.");
        }
    }

    public void SaveGoals(){
        using (StreamWriter writer = new StreamWriter("goals.txt")){
            foreach (Goal goal in _goals){
                writer.WriteLine($"{goal.GetType().Name}|{goal._shortName}|{goal._description}|{goal._points}");
            }
        }

        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals(){
        _goals.Clear();

        if (File.Exists("goals.txt")){
            using (StreamReader reader = new StreamReader("goals.txt")){
                string line;
                while ((line = reader.ReadLine()) != null){
                    string[] parts = line.Split('|');
                    if (parts.Length == 4){
                        string goalType = parts[0];
                        Goal goal;

                        switch (goalType.ToLower()){
                            case "simple":
                                goal = new SimpleGoal();
                                break;

                            case "eternal":
                                goal = new EternalGoal();
                                break;

                            case "checklist":
                                goal = new ChecklistGoal();
                                break;

                            default:
                                continue; // Skip invalid lines
                        }

                        goal._shortName = parts[1];
                        goal._description = parts[2];
                        if (int.TryParse(parts[3], out int points)){
                            goal._points = points;
                        }

                        _goals.Add(goal);
                    }
                }
            }

            Console.WriteLine("Goals loaded successfully.");
        }
        else{
            Console.WriteLine("No saved goals found.");
        }
    }
}

class Program{
    static void Main(){
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}


//Code by Nicolas Velasquez©