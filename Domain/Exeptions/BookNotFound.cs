namespace Domain.Exeptions;

public class BookNotFound : NotFound
{
    public BookNotFound(string msg) : base(msg)
    {
    }
    
}