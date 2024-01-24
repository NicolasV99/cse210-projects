using System;
using System.Collections.Generic;
using System.Linq;

class Program{
    static void Main(){
        
        // There is the scripture
        Reference reference = new Reference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "Trust in the LORD with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.");
        
        while (!scripture.IsCompletelyHidden()){
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            
            Console.WriteLine("Press Enter to hide more words or type 'quit' to exit.");
            string userInput = Console.ReadLine();
            
            if (userInput.ToLower() == "quit")
                break;
            
            int numberOfWordsToHide = 2; // Here we hide 2 words for time
            scripture.HideRandomWords(numberOfWordsToHide);
        }
        
        Console.WriteLine("Goodbye! Thanks for learn!");
    }
}

class Scripture{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text){
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int numberToHide){
        Random random = new Random();

        for (int i = 0; i < numberToHide; i++){
            int randomIndex = random.Next(_words.Count);

            // Check if the word is not already hidden
            if (!_words[randomIndex].IsHidden()){
                _words[randomIndex].Hide();
            }
            else{
                // If the selected word is already hidden, decrement the loop counter for find other not hidden
                i--;
            }
        }
    }

    public string GetDisplayText(){
        return $"{_reference.GetDisplayText()} - {_words.Select(word => word.GetDisplayText()).Aggregate((current, next) => $"{current} {next}")}";
    }

    public bool IsCompletelyHidden(){
        return _words.All(word => word.IsHidden());
    }
}

class Word{
    private string _text;
    private bool _isHidden;

    public Word(string text){
        _text = text;
        _isHidden = false;
    }

    public void Hide(){
        _isHidden = true;
    }

    public void Show(){
        _isHidden = false;
    }

    public bool IsHidden(){
        return _isHidden;
    }

    public string GetDisplayText(){
        return _isHidden ? "_____" : _text;
    }
}

class Reference{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    public Reference(string book, int chapter, int verse){
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse){
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText(){
        return $"{_book} {_chapter}:{_verse}-{_endVerse}";
    }
}

//Code by Nicolas Velasquez©