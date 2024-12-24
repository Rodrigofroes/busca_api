namespace BackAppPersonal.Domain.Exceptions
{
    public class ExceptionService : Exception
    {
        public ExceptionService(string message) : base(message)
        {
        }
    }
}
