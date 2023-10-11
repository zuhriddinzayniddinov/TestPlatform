namespace TestPlatform.Domain.Exceptions;

public class RegisteredException : Exception
{
    public RegisteredException(string message)
        : base(message)
    {
        
    }
}