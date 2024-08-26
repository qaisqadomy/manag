using Application.DTOs;
using Application.Services;

namespace LibraryManagment.EndPoints;

public static class LibraryEndpoints
{
    public static void MapLibraryEndpoints(this WebApplication app)
    {
        var libraryGroup = app.MapGroup("/library").WithTags("Library");
        libraryGroup.MapPost("/borrow", (int bookId, int memberId, LibraryService libraryService) =>
        {
            libraryService.BorrowBook(bookId, memberId);
            return Results.Ok("borrowed successfully");
        });

        libraryGroup.MapPost("/return", (int bookId, LibraryService libraryService) =>
        {
            libraryService.ReturnBook(bookId);
            return Results.Ok("returned successfully");

        });

        libraryGroup.MapGet("/", (LibraryService libraryService) =>
        {
            List<BookDTO> books = libraryService.GetBorrowed();
            return Results.Ok(books);
        });
    }
}