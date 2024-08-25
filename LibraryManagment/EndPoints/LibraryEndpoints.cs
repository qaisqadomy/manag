using Application.IServices;

namespace LibraryManagment.EndPoints;

public static class LibraryEndpoints
{
    public static void MapLibraryEndpoints(this WebApplication app)
    {
        var libraryGroup = app.MapGroup("/library").WithTags("Library");
        libraryGroup.MapPost("/borrow", (int bookId, int memberId, ILibraryService libraryService) =>
        {
            try
            {
                 libraryService.BorrowBook(bookId, memberId);
                return Results.Ok("borrowed successfully");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
        
        libraryGroup.MapPost("/return", (int bookId, ILibraryService libraryService) =>
        {
            try
            {
                libraryService.ReturnBook(bookId);
                return Results.Ok("returned successfully");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}