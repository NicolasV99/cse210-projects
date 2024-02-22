using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Book class
public class Book
{
    public string ISBN { get; }
    public string Title { get; }
    public string Author { get; }
    public string Genre { get; }
    public bool IsAvailable { get; set; }

    public Book(string isbn, string title, string author, string genre)
    {
        ISBN = isbn;
        Title = title;
        Author = author;
        Genre = genre;
        IsAvailable = true;
    }

    public string GetDetails()
    {
        return $"ISBN: {ISBN}, Title: {Title}, Author: {Author}, Genre: {Genre}, Available: {(IsAvailable ? "Yes" : "No")}";
    }
}

// Library class
public class Library
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public Book SearchBook(string isbn)
    {
        return books.FirstOrDefault(book => book.ISBN == isbn);
    }

    public void ListBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine(book.GetDetails());
        }
    }

    // Other methods such as checkout, return, etc. can be added here
}

// Patron class
public class Patron
{
    public string Name { get; }
    public LibraryCard LibraryCard { get; }

    public Patron(string name, LibraryCard libraryCard)
    {
        Name = name;
        LibraryCard = libraryCard;
    }

    // Additional methods can be added here
}

// LibraryCard class
public class LibraryCard
{
    public int CardNumber { get; }
    public Patron Patron { get; }

    public LibraryCard(int cardNumber, Patron patron)
    {
        CardNumber = cardNumber;
        Patron = patron;
    }
}

// Loan class
public class Loan
{
    public Book Book { get; }
    public Patron Patron { get; }
    public DateTime DueDate { get; }

    public Loan(Book book, Patron patron, DateTime dueDate)
    {
        Book = book;
        Patron = patron;
        DueDate = dueDate;
    }

    // Additional methods can be added here
}

// Catalog class
public class Catalog
{
    private List<Book> books;

    public Catalog()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public Book SearchBook(string title)
    {
        return books.FirstOrDefault(book => book.Title == title);
    }

    public void ListBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine(book.GetDetails());
        }
    }
}

class Program{
    static Library library = new Library(); // Initialize a library object

    static void Main(string[] args){
        // Load books from the text file
        LoadBooksFromFile("books.txt");

        bool exit = false;
        while (!exit)
        {
            DisplayMenu();
            int choice = GetMenuChoice();

            switch (choice)
            {
                case 1:
                    SearchForBook();
                    break;
                case 2:
                    AddBook();
                    break;
                case 3:
                    RemoveBook();
                    break;
                case 4:
                    ListAllBooks();
                    break;
                case 5:
                    // Implement Check Out a Book functionality
                    break;
                case 6:
                    // Implement Return a Book functionality
                    break;
                case 7:
                    // Implement List Checked Out Books functionality
                    break;
                case 8:
                    // Implement List Overdue Loans functionality
                    break;
                case 9:
                    exit = true;
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void LoadBooksFromFile(string filename)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] bookInfo = line.Split(',');
                    string isbn = bookInfo[0].Trim();
                    string title = bookInfo[1].Trim();
                    string author = bookInfo[2].Trim();
                    string genre = bookInfo[3].Trim();

                    Book newBook = new Book(isbn, title, author, genre);
                    library.AddBook(newBook);
                }
            }
            Console.WriteLine("Books loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading books from file: {ex.Message}");
        }
    }

    static void DisplayMenu(){
        Console.WriteLine("\nWelcome to the Library System");
        Console.WriteLine("1. Search for a Book");
        Console.WriteLine("2. Add a Book");
        Console.WriteLine("3. Remove a Book");
        Console.WriteLine("4. List all Books");
        Console.WriteLine("5. Check Out a Book");
        Console.WriteLine("6. Return a Book");
        Console.WriteLine("7. List Checked Out Books");
        Console.WriteLine("8. List Overdue Loans");
        Console.WriteLine("9. Exit");
        Console.Write("\nPlease enter your choice: ");
        }

        static int GetMenuChoice(){
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.Write("Invalid input. Please enter a number: ");
        }
        return choice;
        }

        static void SearchForBook()
        {
        Console.Write("Enter ISBN of the book to search: ");
        string isbn = Console.ReadLine();
        Book foundBook = library.SearchBook(isbn);
        if (foundBook != null)
        {
            Console.WriteLine("Book found:");
            Console.WriteLine(foundBook.GetDetails());
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
        }

        static void AddBook()
        {
        Console.WriteLine("Enter book details:");
        Console.Write("ISBN: ");
        string isbn = Console.ReadLine();
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Author: ");
        string author = Console.ReadLine();
        Console.Write("Genre: ");
        string genre = Console.ReadLine();

        Book newBook = new Book(isbn, title, author, genre);
        library.AddBook(newBook);
        Console.WriteLine("Book added successfully.");
        }

        static void RemoveBook()
        {
        Console.Write("Enter ISBN of the book to remove: ");
        string isbn = Console.ReadLine();
        Book foundBook = library.SearchBook(isbn);
        if (foundBook != null)
        {
            library.RemoveBook(foundBook);
            Console.WriteLine("Book removed successfully.");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
        }

        static void ListAllBooks()
        {
        Console.WriteLine("Listing all books:");
        library.ListBooks();
        }

        static void InitializeLibraryWithSampleData()
        {
        // Add sample books to the library for testing
        Book book1 = new Book("1234567890", "The Great Gatsby", "F. Scott Fitzgerald", "Classic");
        Book book2 = new Book("0987654321", "To Kill a Mockingbird", "Harper Lee", "Fiction");
        library.AddBook(book1);
        library.AddBook(book2);
        }
}
