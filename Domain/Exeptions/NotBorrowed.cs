namespace Domain.Exeptions;

public class NotBorrowed : InvalidOperation
{
    public NotBorrowed(string msg) : base(msg)
    {
        
    }
    
}