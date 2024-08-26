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
            throw new BookNotFound($"Book with the Id :{id} doesn't exist");
        }

        if (bookRepo.IsBorrowed(book.Id))
        {
            throw new AlreadyBorrowed($"Book is with the Id : {id} already borrowed.");
        }

        bookRepo.Borrow(book, memberId);
    }

    public void ReturnBook(int id)
    {
        List<Book> books = bookRepo.GetAll();
        Book book = books.FirstOrDefault(b => b.Id == id)!;
        if (book == null)
        {
            throw new BookNotFound($"Book with the Id : {id} doesn't exist");
        }

        if (!book.IsBorrowed)
        {
            throw new NotBorrowed($"Book with the id: {id} is not borrowed.");
        }

        bookRepo.Return(book);
    }
    
    public List<Book> GetBorrowed()
    {
        List<Book> books = bookRepo.GetAll();
        List<Book> borrowed = new List<Book>();
        foreach(Book b in books)
        {
            if(b.IsBorrowed) borrowed.Add(b);
        }
        return borrowed;
    }
}