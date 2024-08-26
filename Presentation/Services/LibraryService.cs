using Application.DTOs;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;

namespace Application.Services;

public class LibraryService(ILibraryRepo libraryRepo, IMemberRepo memberRepo, IBookRepo bookRepo)
{
    public void BorrowBook(int bookId, int memberId)
    {
        Book book = (bookRepo.GetAll().FirstOrDefault(b => b.Id == bookId) ?? throw new NotFound("no books "))!;
        Member member = (memberRepo.GetAll().FirstOrDefault(m => m.Id == memberId) ?? throw new NotFound("no members"))!;

        libraryRepo.BorrowBook(bookId, memberId);
    }

    public void ReturnBook(int bookId)
    {
        Book book = (bookRepo.GetAll().FirstOrDefault(b => b.Id == bookId) ?? throw new BookNotFound("no books"))!;

        libraryRepo.ReturnBook(bookId);
    }

    public List<BookDTO> GetBorrowed()
    {
        List<Book> books = libraryRepo.GetBorrowed();
        return books.ConvertAll(book => new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            IsBorrowed = book.IsBorrowed,
            BorrowedDate = book.BorrowedDate,
            BorrowedBy = book.BorrowedBy
        });
    }
}