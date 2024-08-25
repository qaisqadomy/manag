using Application.DTOs;

namespace Application.IServices;

public interface IBookService
{
    void Add(BookDto model);
    List<BookDto> GetAll();
    void Remove(int bookId);
    void Update(BookDto model);
    BookDto Find(int bookId);
}