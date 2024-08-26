using Application.DTOs;
using Application.Services;

namespace LibraryManagment.EndPoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        var booksGroup = app.MapGroup("/Books")
            .WithTags("Books");

        booksGroup.MapGet("/GetAll", (BookService bookService) =>
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
        booksGroup.MapGet("/Get{id}", (int bookId, BookService bookService) =>
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
        booksGroup.MapPost("/add", (BookDto model, BookService bookService) =>
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
        booksGroup.MapDelete("/remove", (int bookId, BookService bookService) =>
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
        booksGroup.MapPut("/update", (BookDto model, BookService bookService) =>
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