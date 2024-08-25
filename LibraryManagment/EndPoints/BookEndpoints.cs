using Application.DTOs;
using Application.IServices;

namespace LibraryManagment.EndPoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        var booksGroup = app.MapGroup("/Books")
            .WithTags("Books");

        booksGroup.MapGet("/GetAll", (IBookService bookService) =>
        {
            try
            {
                List<BookDto> list = bookService.GetAll();
                return Results.Ok(list);
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
        booksGroup.MapGet("/Get{id}", (int bookId, IBookService bookService) =>
        {
            try
            {
                BookDto book = bookService.Find(bookId);
                return Results.Ok(book);
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
        booksGroup.MapPost("/add", (BookDto model, IBookService bookService) =>
        {
            try
            {
                bookService.Add(model);
                return Results.Ok("added successfully");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
        booksGroup.MapDelete("/remove", (int bookId, IBookService bookService) =>
        {
            try
            {
                bookService.Remove(bookId);
                return Results.Ok("removed successfully");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
        booksGroup.MapPut("/update", (BookDto model, IBookService bookService) =>
        {
            try
            {
                bookService.Update(model);
                return Results.Ok("updated successfully");
            }
            catch (Exception e)
            {
                return Results.NotFound( e.Message );
            }
        });
    }
}