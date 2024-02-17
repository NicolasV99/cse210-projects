using System;
using System.Collections.Generic;
using System.Linq;


public class Book{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public bool IsAvailable { get; set; }

    public Book(string isbn, string title, string author, string genre, bool isAvailable){
        ISBN = isbn;
        Title = title;
        Author = author;
        Genre = genre;
        IsAvailable = isAvailable;
    }

    public string GetDetails(){
        return $"Title: {Title}\nAuthor: {Author}\nGenre: {Genre}\nISBN: {ISBN}\nAvailable: {IsAvailable}";
    }
}

public class Library{
    private List<Book> books;

    public Library(){
        books = new List<Book>();
    }

    public void AddBook(Book book){
        books.Add(book);
    }

    public void RemoveBook(Book book){
        books.Remove(book);
    }

    public Book SearchBook(string isbn){
        return books.Find(book => book.ISBN == isbn);
    }

    public void ListBooks(){
        if (books.Count > 0){
            foreach (var book in books){
                Console.WriteLine(book.GetDetails());
                Console.WriteLine("-----------------------");
            }
        }
        else{
            Console.WriteLine("No books available in the library.");
        }
    }

    public void CheckoutBook(Book book, Patron patron){
        if (books.Contains(book) && book.IsAvailable){
            book.IsAvailable = false;
            Console.WriteLine($"Book '{book.Title}' checked out to {patron.Name}.");
        }
        else{
            Console.WriteLine("Book is not available for checkout.");
        }
    }

    public void ReturnBook(Book book){
        if (books.Contains(book)){
            book.IsAvailable = true;
            Console.WriteLine($"Book '{book.Title}' returned successfully.");
        }
        else{
            Console.WriteLine("Book not found in the library.");
        }
    }
}

public class Patron{
    private List<Book> checkedOutBooks;
    public string Name { get; private set; }

    public Patron(string name){
        Name = name;
        checkedOutBooks = new List<Book>();
    }

    public void CheckOutBook(Book book){
        checkedOutBooks.Add(book);
    }

    public void ReturnBook(Book book){
        checkedOutBooks.Remove(book);
    }

    public List<Book> GetCheckedOutBooks(){
        return checkedOutBooks;
    }
}

public class LibraryCard{
    public string CardNumber { get; set; }
    public Patron Patron { get; set; }

    public LibraryCard(string cardNumber, Patron patron){
        CardNumber = cardNumber;
        Patron = patron;
    }
}

public class Loan{
    public Book Book { get; set; }
    public Patron Patron { get; set; }
    public DateTime DueDate { get; set; }

    public Loan(Book book, Patron patron, DateTime dueDate){
        Book = book;
        Patron = patron;
        DueDate = dueDate;
    }
}

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

    public void DisplayCatalog()
    {
        Console.WriteLine("Catalog:");
        foreach (var book in books){
            Console.WriteLine(book);
        }
    }

    public List<Book> SearchByTitle(string title){
        return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Book> SearchByAuthor(string author){
        return books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Book> SearchByGenre(string genre){
        return books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Book> GetAllBooks(){
        return books;
    }
    public Book FindBookByISBN(string isbn){
        foreach (var book in books){
            if (book.ISBN == isbn){
                return book;
            }
        }
        return null; // Book not found
    }

    public bool CheckOutBook(Book book){
        if (book.IsAvailable){
            book.IsAvailable = false;
            return true; // Book checked out successfully
        }
        else{
            return false; // Book already checked out
        }
    }

    public bool ReturnBook(Book book){
        if (!book.IsAvailable){
            book.IsAvailable = true;
            return true; // Book returned successfully
        }
        else{
            return false; // Book already available
        }
    }
}



class Program{
    static void Main(){
        // Create a new library catalog
        Catalog catalog = new Catalog();

        // Load books from CSV file
        LoadBooksFromCSV("books.csv", catalog);

        
        // Sample patrons
        Patron patron1 = new Patron("John Doe");
        Patron patron2 = new Patron("Jane Smith");

        // Main menu loop
        while (true){
            Console.Clear();
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. View Catalog");
            Console.WriteLine("2. Check Out Book");
            Console.WriteLine("3. Return Book");
            Console.WriteLine("4. View Patron's Checked Out Books");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice)){
                switch (choice){
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Catalog:");
                        catalog.DisplayCatalog();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Check Out Book:");
                        Console.Write("Enter book ISBN: ");
                        string isbn = Console.ReadLine();
                        Book bookToCheckOut = catalog.FindBookByISBN(isbn);
                        if (bookToCheckOut != null){
                            catalog.CheckOutBook(bookToCheckOut);
                            patron1.CheckOutBook(bookToCheckOut);
                            Console.WriteLine("Book checked out successfully.");
                        }
                        else{
                            Console.WriteLine("Book not found.");
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Return Book:");
                        Console.Write("Enter book ISBN: ");
                        isbn = Console.ReadLine();
                        Book bookToReturn = catalog.FindBookByISBN(isbn);
                        if (bookToReturn != null){
                            catalog.ReturnBook(bookToReturn);
                            patron1.ReturnBook(bookToReturn);
                            Console.WriteLine("Book returned successfully.");
                        }
                        else{
                            Console.WriteLine("Book not found.");
                        }
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine($"Books checked out by {patron1.Name}:");
                        foreach (var book in patron1.GetCheckedOutBooks()){
                            Console.WriteLine($"- {book.Title} by {book.Author}");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
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

    static void LoadBooksFromCSV(string filePath, Catalog catalog){
        if (File.Exists(filePath)){
            using (StreamReader reader = new StreamReader(filePath)){
                string line;
                while ((line = reader.ReadLine()) != null){
                    string[] parts = line.Split(',');
                    if (parts.Length == 5){
                        string isbn = parts[0];
                        string title = parts[1];
                        string author = parts[2];
                        string genre = parts[3];
                        bool isAvailable = bool.Parse(parts[4]);
                        Book book = new Book(isbn, title, author, genre, isAvailable);
                        catalog.AddBook(book);
                    }
                }
            }
        }
        else{
            Console.WriteLine($"File not found: {filePath}");
        }
    }

}


//Code by Nicolas Velasquez