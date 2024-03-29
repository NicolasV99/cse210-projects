using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Book class
public class Book{
    public string ISBN { get; }
    public string Title { get; }
    public string Author { get; }
    public string Genre { get; }
    public bool IsAvailable { get; set; }

    public Book(string isbn, string title, string author, string genre){
        ISBN = isbn;
        Title = title;
        Author = author;
        Genre = genre;
        IsAvailable = true;
    }

    public string GetDetails(){
        return $"ISBN: {ISBN}, Title: {Title}, Author: {Author}, Genre: {Genre}, Available: {(IsAvailable ? "Yes" : "No")}";
    }
}

// Library class
public class Library
{
    private List<Book> books;
    private List<Loan> loans; // List to track all loans

    public Library(){
        books = new List<Book>();
        loans = new List<Loan>();
    }

    public void AddBook(Book book){
        books.Add(book);
    }

    public void RemoveBook(Book book){
        books.Remove(book);
    }

    public Book SearchBook(string isbn){
        return books.FirstOrDefault(book => book.ISBN == isbn);
    }

    public void ListBooks(){
        foreach (var book in books){
            Console.WriteLine(book.GetDetails());
        }
    }

    public bool CheckOutBook(Book book, Patron patron, int loanDurationInDays){
        if (book.IsAvailable){
            book.IsAvailable = false; // Mark the book as unavailable
            DateTime dueDate = DateTime.Now.AddDays(loanDurationInDays);
            Loan loan = new Loan(book, patron, dueDate);
            loans.Add(loan); // Add the loan to the list of loans
            patron.AddLoan(loan); // Add the loan to the patron's list of loans
            return true;
        }
        else{
            return false; // Book is not available for checkout
        }
    }    

    public bool ReturnBook(Book book, Patron patron){
        // Find the loan associated with the book and patron
        Loan loan = loans.FirstOrDefault(l => l.Book == book && l.Patron == patron);
        if (loan != null){
            // Remove the loan from both the library's list of loans and the patron's list of loans
            loans.Remove(loan);
            patron.Loans.Remove(loan);
            // Mark the book as available again
            book.IsAvailable = true;
            return true;
        }
        else{
            return false; // Book was not checked out by the patron
        }
    }

    public List<Book> GetCheckedOutBooks(){
        // Get a list of all books that are currently checked out
        return loans.Where(loan => loan.Book.IsAvailable == false).Select(loan => loan.Book).ToList();
    }

    public List<Loan> GetOverdueLoans(){
        // Get a list of all loans that are currently overdue
        return loans.Where(loan => loan.IsOverdue()).ToList();
    }    

}

// Patron class
public class Patron{
    public string Name { get; }
    public LibraryCard LibraryCard { get; }
    public List<Loan> Loans { get; } // List to track loans associated with the patron

    public Patron(string name, LibraryCard libraryCard){
        Name = name;
        LibraryCard = libraryCard;
        Loans = new List<Loan>(); // Initialize the list of loans
    }

    public void AddLoan(Loan loan){
        Loans.Add(loan);
    }

}

// LibraryCard class
public class LibraryCard{
    public int CardNumber { get; }
    public Patron Patron { get; }

    public LibraryCard(int cardNumber, Patron patron){
        CardNumber = cardNumber;
        Patron = patron;
    }
}

// Loan class
public class Loan{
    public Book Book { get; }
    public Patron Patron { get; }
    public DateTime DueDate { get; }

    public Loan(Book book, Patron patron, DateTime dueDate){
        Book = book;
        Patron = patron;
        DueDate = dueDate;
    }

    public bool IsOverdue(){
        return DateTime.Now > DueDate;
    }
}




// Catalog class
public class Catalog{
    private List<Book> books;

    public Catalog(){
        books = new List<Book>();
    }

    public void AddBook(Book book){
        books.Add(book);
    }

    public void RemoveBook(Book book){
        books.Remove(book);
    }

    public Book SearchBook(string title){
        return books.FirstOrDefault(book => book.Title == title);
    }

    public void ListBooks(){
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
        while (!exit){
            DisplayMenu();
            int choice = GetMenuChoice();

            switch (choice){
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
                    CheckOutBook();
                    break;
                case 6:
                    ReturnBook();
                    break;
                case 7:
                    ListCheckedOutBooks();
                    break;
                case 8:
                    ListOverdueLoans();
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

    static void LoadBooksFromFile(string filename){
        try{
            using (StreamReader sr = new StreamReader(filename)){
                string line;
                while ((line = sr.ReadLine()) != null){
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
        catch (Exception ex){
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
        while (!int.TryParse(Console.ReadLine(), out choice)){
            Console.Write("Invalid input. Please enter a number: ");
        }
        return choice;
    }

    static void SearchForBook(){
        Console.Write("Enter ISBN of the book to search: ");
        string isbn = Console.ReadLine();
        Book foundBook = library.SearchBook(isbn);
        if (foundBook != null){
            Console.WriteLine("Book found:");
            Console.WriteLine(foundBook.GetDetails());
        }
        else{
            Console.WriteLine("Book not found.");
        }
    }

    static void AddBook(){
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

    static void RemoveBook(){
        Console.Write("Enter ISBN of the book to remove: ");
        string isbn = Console.ReadLine();
        Book foundBook = library.SearchBook(isbn);
        if (foundBook != null){
            library.RemoveBook(foundBook);
            Console.WriteLine("Book removed successfully.");
        }
        else{
            Console.WriteLine("Book not found.");
        }
    }

    static void ListAllBooks(){
        Console.WriteLine("Listing all books:");
        library.ListBooks();
    }


    static void CheckOutBook(){
        Console.Write("Enter ISBN of the book to check out: ");
        string isbn = Console.ReadLine();
        Book book = library.SearchBook(isbn);
        if (book != null){
            Console.Write("Enter patron's name: ");
            string patronName = Console.ReadLine();
            Patron patron = GetOrCreatePatron(patronName);
            if (patron != null){
                if (library.CheckOutBook(book, patron, 5)) // Assuming a 14-day loan period
                {
                    Console.WriteLine($"Book '{book.Title}' checked out successfully by {patron.Name}.");
                }
                else{
                    Console.WriteLine("Book is not available for checkout.");
                }
            }
            else{
                Console.WriteLine("Failed to retrieve/create patron.");
            }
        }
        else{
            Console.WriteLine("Book not found.");
        }
    }

    static void ReturnBook(){
        Console.Write("Enter ISBN of the book to return: ");
        string isbn = Console.ReadLine();
        Book book = library.SearchBook(isbn);
        if (book != null){
            Console.Write("Enter patron's name: ");
            string patronName = Console.ReadLine();
            Patron patron = GetOrCreatePatron(patronName);
            if (patron != null){
                if (library.ReturnBook(book, patron)){
                    Console.WriteLine($"Book '{book.Title}' returned successfully by {patron.Name}.");
                }
                else{
                    Console.WriteLine($"Book '{book.Title}' was not checked out by {patron.Name}.");
                }
            }
            else{
                Console.WriteLine("Failed to retrieve/create patron.");
            }
        }
        else{
            Console.WriteLine("Book not found.");
        }
    }   

    static void ListCheckedOutBooks(){
        List<Book> checkedOutBooks = library.GetCheckedOutBooks();
        Console.WriteLine("Checked Out Books:");
        foreach (var book in checkedOutBooks){
            Console.WriteLine(book.GetDetails());
        }
    }

    static void ListOverdueLoans(){
        List<Loan> overdueLoans = library.GetOverdueLoans();
        Console.WriteLine("Overdue Loans:");
        foreach (var loan in overdueLoans){
            Console.WriteLine($"Book: {loan.Book.Title}, Patron: {loan.Patron.Name}, Due Date: {loan.DueDate}");
        }
    }

    static Patron GetOrCreatePatron(string name){
        // Logic to get or create a patron by name
        // For simplicity, we'll return a new patron object for each name entered
        return new Patron(name, new LibraryCard(111, null)); // Using a fictitious library card number
        //Please when you check out for a book, this would be the patron's name (111)
    }
}

//Code by Nicolas Velasquez©


/*There is some instructions for use the program:
 
-Options:
Choose an option by entering the corresponding number and pressing Enter.

-Search for a Book:
Enter the ISBN of the book you want to search for. The program will display details if the book is found.

-Add a Book:
Enter the details (ISBN, Title, Author, Genre) of the book you want to add. The book will be added to the library's collection.

-Remove a Book:
Enter the ISBN of the book you want to remove. If the book is found, it will be removed from the library's collection.

-List all Books:
Displays details of all books available in the library.

-Check Out a Book:
Enter the ISBN of the book you want to check out and the name of the patron checking it out. The program will attempt to check out the book.

-Return a Book:
Enter the ISBN of the book being returned and the name of the patron returning it. The program will attempt to return the book to the library.

-List Checked Out Books:
Displays details of all books currently checked out from the library.

-List Overdue Loans:
Displays details of all loans that are currently overdue. 
*/