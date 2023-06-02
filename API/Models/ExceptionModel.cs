namespace Domain.Models;

public class ExceptionModel
{
    public ExceptionModel(string message)
    {
        Message = message;
    }

    public string Message { get; set; }

}
