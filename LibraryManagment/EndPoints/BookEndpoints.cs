using Application.DTOs;
using Application.Services;
using FluentValidation;

namespace LibraryManagment.EndPoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        var booksGroup = app.MapGroup("/books")
            .WithTags("Books");

        booksGroup.MapGet("/", (BookService bookService) =>
        {

            List<BookDTO> list = bookService.GetAll();
            return Results.Ok(list);

        });
        booksGroup.MapGet("/{id}", (int bookId, BookService bookService) =>
        {
            BookDTO book = bookService.Find(bookId);
            return Results.Ok(book);
        });
        booksGroup.MapPost("/", (BookDtoUpdate model, BookService bookService) =>
        {
            
            bookService.Add(model);
            return Results.Created();

        });
        booksGroup.MapDelete("/", (int bookId, BookService bookService) =>
        {
            bookService.Remove(bookId);
            return Results.NoContent();

        });
        booksGroup.MapPut("/", (BookDtoUpdate model, BookService bookService) =>
        {
            bookService.Update(model);
            return Results.Ok("updated successfully");
        });
    }
}