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
            try
            {
                 libraryService.BorrowBook(bookId, memberId);
                return Results.Ok("borrowed successfully");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
        
        libraryGroup.MapPost("/return", (int bookId, LibraryService libraryService) =>
        {
            try
            {
                libraryService.ReturnBook(bookId);
                return Results.Ok("returned successfully");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });

        libraryGroup.MapGet("/getBorrowed", (LibraryService libraryService) =>
        {
            try
            {
                List<BookDto> books= libraryService.GetBorrowed();
                return Results.Ok(books);
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}