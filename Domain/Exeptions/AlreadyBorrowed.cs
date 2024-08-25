namespace Domain.Exeptions;

public class AlreadyBorrowed : InvalidOperation
{
    public AlreadyBorrowed(string msg ): base(msg)
    {
        
    }
    
}