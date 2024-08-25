using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;
using Infrastructure;

namespace Application.Repos;

public class BookRepo(AppDbContext context) : IBookRepo
{
    public void Add(Book book)
    {
        if (context.Books.Any(b => b.Author == book.Author && b.Title == book.Title))
            throw new BookExists("Book already exists");

        context.Books.Add(book);
        context.SaveChanges();
    }

    public void Remove(int bookId)
    {
            Book book = context.Books.Find(bookId) ?? throw new BookNotFound("Book not found");

            context.Books.Remove(book);
            context.SaveChanges();
    }

    public void Update(Book book)
    {
        Book existingBook = context.Books.Find(book.Id) ?? throw new BookNotFound("Book not found");
        context.Entry(existingBook).CurrentValues.SetValues(book);
        context.SaveChanges();
    }

    public Book Find(int bookId)
    {
        Book book = context.Books.Find(bookId) ?? throw new BookNotFound("Book not found");
        return book;
    }

    public List<Book> GetAll()
    {
        return context.Books.ToList();
    }

    public bool IsBorrowed(int bookid)
    {
        Book boo = context.Books.Find(bookid) ?? throw new BookNotFound("Book not found");
        return boo.IsBorrowed;
    }

    public void Borrow(Book book, int memberId)
    {
        Book b = context.Books.FirstOrDefault(b => b.Id == book.Id)
                 ?? throw new BookNotFound("Book not found");

        if (IsBorrowed(b.Id))
        {
            throw new AlreadyBorrowed("Book is borrowed");
        }

        b.IsBorrowed = true;
        b.BorrowedBy = memberId;
        b.BorrowedDate = DateTime.UtcNow;

        context.Books.Update(b);
        context.SaveChanges();
    }

    public void Return(Book book)
    {
        var b = context.Books.FirstOrDefault(b => b.Id == book.Id)
                ?? throw new BookNotFound("Book not found");

        if (!IsBorrowed(b.Id))
        {
            throw new NotBorrowed("Book isn't borrowed");
        }

        b.IsBorrowed = false;
        b.BorrowedBy = null;
        b.BorrowedDate = null;

        context.Books.Update(b);
        context.SaveChanges();
    }
}