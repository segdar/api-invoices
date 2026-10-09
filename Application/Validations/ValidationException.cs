namespace ApiInvoices.Application.Validations;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
