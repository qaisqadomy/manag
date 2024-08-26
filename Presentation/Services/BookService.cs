using Application.DTOs;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;

namespace Application.Services;

public class BookService(IBookRepo repo) 
{
    public void Add(BookDtoUpdate model)
    {
        Book book = new Book
        {
            Title = model.Title,
            Author = model.Author,
            IsBorrowed = model.IsBorrowed,
            BorrowedDate = model.BorrowedDate,
            BorrowedBy = model.BorrowedBy
        };
        repo.Add(book);
    }

    public List<BookDTO> GetAll()
    {
        List<Book> books = repo.GetAll();
        if (books.Count == 0)
        {
            return new List<BookDTO>();
        }

        return books.ConvertAll(book => new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            IsBorrowed = book.IsBorrowed,
            BorrowedDate = book.BorrowedDate,
            BorrowedBy = book.BorrowedBy
        }).ToList();
    }

    public void Remove(int id)
    {
        repo.Remove(id);
    }

    public void Update(BookDtoUpdate model)
    {
        Book book = new Book
        {
            Author = model.Author,
            Title = model.Title
        };
        repo.Update(book);
    }

    public BookDTO Find(int id)
    {
        Book book = repo.Find(id)!;
        
        BookDTO bookDTO = new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            BorrowedBy = book.BorrowedBy,
            BorrowedDate = book.BorrowedDate,
            IsBorrowed = book.IsBorrowed
        };
        return bookDTO;
    }
}