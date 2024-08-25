using Domain.Entities;

namespace Domain.IRepo;

public interface ILibraryRepo
{
    void BorrowBook(int bookId, int memberId);
    void ReturnBook(int bookId);
    public List<Book> GetBorrowed();
}