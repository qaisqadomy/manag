using Application.DTOs;
using Application.Services;
using LibraryManagment.Validators;

namespace LibraryManagment.EndPoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        BookDtoCreateValidator validationRules = new BookDtoCreateValidator();

        RouteGroupBuilder booksGroup = app.MapGroup("/books")
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
        booksGroup.MapPost("/", (BookDtoCreate model, BookService bookService) =>
        {
            if (!validationRules.Validate(model).IsValid)
            {
                return Results.BadRequest(model);
            }
            bookService.Add(model);
            return Results.Ok();
        });
        booksGroup.MapDelete("/", (int bookId, BookService bookService) =>
        {
            bookService.Remove(bookId);
            return Results.Ok();

        });
        booksGroup.MapPut("/", (BookDtoCreate model,int bookId, BookService bookService) =>
        {

            if (!validationRules.Validate(model).IsValid)
            {
                return Results.BadRequest();
            }

            bookService.Update(model,bookId);
            return Results.Ok(model);
        });
    }
}