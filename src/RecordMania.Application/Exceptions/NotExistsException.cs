namespace RecordMania.Application.Exceptions;

public class NotExistsException : Exception
{
    
    public NotExistsException(string msg) : base(msg) { }
}