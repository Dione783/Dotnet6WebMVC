namespace WebApplicationRazor.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string Message): base(Message)
        {

        }
    }
}