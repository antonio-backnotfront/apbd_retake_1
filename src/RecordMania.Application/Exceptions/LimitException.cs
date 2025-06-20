namespace RecordMania.Application.Exceptions;

public class LimitException : Exception
{
    
    public LimitException(string msg) : base(msg) { }
}