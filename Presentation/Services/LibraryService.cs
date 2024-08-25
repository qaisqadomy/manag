using Application.IServices;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;

namespace Application.Services;

public class LibraryService(ILibraryRepo libraryRepo, IMemberRepo memberRepo, IBookRepo bookRepo) : ILibraryService
{
    public void BorrowBook(int bookId, int memberId)
    {
        Book book = (bookRepo.GetAll() ?? throw new NotFound("no books ")).FirstOrDefault(b => b.Id == bookId)!;
        Member member =
            (memberRepo.GetAll() ?? throw new NotFound("no members")).FirstOrDefault(m => m.Id == memberId)!;

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
        Book book = (bookRepo.GetAll() ?? throw new NotFound("no books")).FirstOrDefault(b => b.Id == bookId)!;

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
}