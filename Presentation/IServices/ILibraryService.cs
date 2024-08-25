namespace Application.IServices;

public interface ILibraryService
{
    void BorrowBook(int bookId, int memberId);
    void ReturnBook(int bookId);
}