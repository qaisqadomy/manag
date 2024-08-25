using Application.DTOs;
using Application.IServices;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;

namespace Application.Services;

public class LibraryService(ILibraryRepo libraryRepo, IMemberRepo memberRepo, IBookRepo bookRepo) : ILibraryService
{
    public void BorrowBook(int bookId, int memberId)
    {
        Book book = (bookRepo.GetAll().FirstOrDefault(b => b.Id == bookId) ?? throw new NotFound("no books "))!;
        Member member =
            (memberRepo.GetAll().FirstOrDefault(m => m.Id == memberId) ?? throw new NotFound("no members"))!;

        if (book == null)
        {
            throw new NotFound("book Doesnt Exist");
        }

        if (member == null)
        {
            throw new NotFound("Member Doesnt Exist");
        }

        if (book.IsBorrowed)
        {
            throw new InvalidOperation("Book is already borrowed.");
        }

        libraryRepo.BorrowBook(bookId, memberId);
    }

    public void ReturnBook(int bookId)
    {
        Book book = (bookRepo.GetAll().FirstOrDefault(b => b.Id == bookId) ?? throw new NotFound("no books"))!;

        if (book == null)
        {
            throw new NotFound("book not found");
        }

        if (!book.IsBorrowed)
        {
            throw new InvalidOperation("Book is not borrowed.");
        }

        libraryRepo.ReturnBook(bookId);
    }
    
    public List<BookDto> GetBorrowed()
    {
        List<Book> books = libraryRepo.GetBorrowed();
        return books.ConvertAll(book =>  new BookDto{
            Id = book.Id,
            Title = book.Title,
            Author =book.Author,
            IsBorrowed = book.IsBorrowed,
            BorrowedDate = book.BorrowedDate,
            BorrowedBy = book.BorrowedBy
        });
    }
}