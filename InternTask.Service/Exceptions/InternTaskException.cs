namespace InternTask.Service.Exceptions;
public class InternTaskException : Exception
{
    public int Code { get; set; }
    public InternTaskException(int code = 500, string message = "Something went wrong") : base(message)
    {
        this.Code = code;
    }
}
