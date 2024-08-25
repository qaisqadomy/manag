using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;

namespace Application.Repos;

public class LibraryRepo(
    IBookRepo bookRepo)
    : ILibraryRepo
{
    public void BorrowBook(int id, int memberId)
    {
        List<Book> books = bookRepo.GetAll();

        Book book = books.FirstOrDefault(b => b.Id == id)!;
        if (book == null)
        {
            throw new BookNotFound("Book not found.");
        }

        if (bookRepo.IsBorrowed(book.Id))
        {
            throw new AlreadyBorrowed("Book is already borrowed.");
        }

        bookRepo.Borrow(book, memberId);
    }

    public void ReturnBook(int id)
    {
        List<Book> books = bookRepo.GetAll();
        Book book = books.FirstOrDefault(b => b.Id == id)!;
        if (book == null)
        {
            throw new BookNotFound("Book not found.");
        }

        if (!book.IsBorrowed)
        {
            throw new NotBorrowed("Book is not currently borrowed.");
        }

        bookRepo.Return(book);
    }
    
    public List<Book> GetBorrowed()
    {
        List<Book> books = (bookRepo.GetAll() ?? throw new NotFound("no books"))!;
        List<Book> borrowed = new List<Book>();
        foreach(Book b in books)
        {
            if(b.IsBorrowed) borrowed.Add(b);
        }
        return borrowed;
    }
}