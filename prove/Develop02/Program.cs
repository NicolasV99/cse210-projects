using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks.Dataflow;


class Entry{
    private string _date;
    private string _prompText;
    private string _entryText;

    public Entry(string date, string prompText, string entryText){
        _date = date;
        _prompText = prompText;
        _entryText = entryText;
    }

    public void Display(){
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompText}");
        Console.WriteLine($"Entry: {_entryText}");
    }

    public string FormatForFile(){
        return $"{_date}~|~{_prompText}~|~{_entryText}";
        
    }
}

class Journal{
    
    private List<Entry> _entries = new List<Entry>();
    public void AddEntry(Entry newEntry){
        _entries.Add(newEntry);
    }
    
    public void DisplayAll(){
        foreach(var entry in _entries){
            entry.Display();
        }
    }

    public void SaveToFile(string file){
        using(StreamWriter writer = new StreamWriter(file)){
            foreach(var entry in _entries){
                writer.WriteLine(entry.FormatForFile());
            }
        }
    }

    public void LoadFromFile(string file){
        _entries.Clear();

        using (StreamReader reader = new StreamReader(file)){
            string line;
            while ((line = reader.ReadLine()) != null){
                string[] parts = line.Split("~|~");
                if(parts.Length == 3){
                    Entry loadedEntry = new Entry(parts[0], parts[1], parts[2]);
                    _entries.Add(loadedEntry);
                }
            }
        }
    }

}

class PrompGenerator{
    private List<string> _prompts = new List<string>{
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What did you do for fun today?",
        "What can you do better tomorrow?"
    };

    public string GetRandomPrompt(){
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}

class Program{
    static void Main(){
        Journal journal = new Journal();
        PrompGenerator prompGenerator = new PrompGenerator();

        while(true){
            Console.WriteLine("Welcome to the Journal Program!");
            Console.WriteLine("Please select one of the following options to start: ");
            Console.WriteLine("1. Write\n2. Display\n3. Load\n4. Save\n5. Exit");

            string choice = Console.ReadLine();

            switch(choice){
                case "1":
                    string prompt = prompGenerator.GetRandomPrompt();
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.WriteLine("Your response: ");
                    string response = Console.ReadLine();
                    Entry newEntry = new Entry(DateTime.Now.ToString(), prompt, response);
                    journal.AddEntry(newEntry);
                    break;
                
                case "2":
                    journal.DisplayAll();
                    break;
                
                case "3":
                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;
                
                case "4":
                    Console.Write("Enter filename to save: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    Console.Write("File Saved Successfully");
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            
            }

        }
    }
}