using Application.DTOs;
using Domain.Entities;

namespace Application.IServices;

public interface ILibraryService
{
    void BorrowBook(int bookId, int memberId);
    void ReturnBook(int bookId);
    public List<BookDto> GetBorrowed();
}