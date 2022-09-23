namespace WebApplicationRazor.Services.Exceptions;

public class IntegrityException : ApplicationException{
    public IntegrityException(string Message) : base(Message){
        
    }
}