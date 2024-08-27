using Application.DTOs;
using Application.Services;

namespace LibraryManagment.EndPoints;

public static class LibraryEndpoints
{
    public static void MapLibraryEndpoints(this WebApplication app)
    {
        RouteGroupBuilder libraryGroup = app.MapGroup("/library").WithTags("Library");
        libraryGroup.MapPost("/borrow", (int bookId, int memberId, LibraryService libraryService) =>
        {
            libraryService.BorrowBook(bookId, memberId);
            return Results.Ok();
        });

        libraryGroup.MapPost("/return", (int bookId, LibraryService libraryService) =>
        {
            libraryService.ReturnBook(bookId);
            return Results.Ok();

        });

        libraryGroup.MapGet("/", (LibraryService libraryService) =>
        {
            List<BookDTO> books = libraryService.GetBorrowed();
            return Results.Ok(books);
        });
    }
}