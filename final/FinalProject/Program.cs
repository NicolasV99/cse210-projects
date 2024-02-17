using System;
using System.Collections.Generic;

public class Book{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public bool IsAvailable { get; set; }

    public Book(string isbn, string title, string author, string genre){
        ISBN = isbn;
        Title = title;
        Author = author;
        Genre = genre;
        IsAvailable = true;
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
    public string Name { get; set; }
    public LibraryCard LibraryCard { get; set; }

    public Patron(string name, LibraryCard libraryCard){
        Name = name;
        LibraryCard = libraryCard;
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

    public List<Book> SearchBooksByTitle(string title){
        return books.FindAll(book => book.Title.Contains(title));
    }

    public List<Book> SearchBooksByAuthor(string author){
        return books.FindAll(book => book.Author.Contains(author));
    }

    public List<Book> SearchBooksByGenre(string genre){
        return books.FindAll(book => book.Genre.Contains(genre));
    }
}

//Code by Nicolas Velasquez