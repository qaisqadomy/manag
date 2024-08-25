namespace Domain.Exeptions;

public class MemberExist : InvalidOperation
{
    public MemberExist( string msg) : base(msg)
    {
        
    }
    
}