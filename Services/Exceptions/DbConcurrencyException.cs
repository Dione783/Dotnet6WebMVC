namespace WebApplicationRazor.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string Message) : base(Message)
        {

        }
    }
}
