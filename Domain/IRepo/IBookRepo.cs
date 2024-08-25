using Domain.Entities;

namespace Domain.IRepo;

public interface IBookRepo
{
    List<Book> GetAll();
    void Add(Book book);
    void Remove(int bookId);
    void Update(Book book);
    Book? Find(int bookId);
    bool IsBorrowed(int bookId);
    void Borrow(Book book, int id);
    void Return(Book book);
}