namespace Domain.Exeptions;

public class BookExists : InvalidOperation
{
    public BookExists(string msg) : base(msg)
    {
        
    }
    
}