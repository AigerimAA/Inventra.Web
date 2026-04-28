namespace Inventra.Application.Common.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException()
            : base("The record was modified by another user. Please reload and try again") { }
    }
}
