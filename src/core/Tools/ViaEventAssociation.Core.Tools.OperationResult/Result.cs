namespace ViaEventAssociation.Core.Tools.Result;

public class Result<T>
{
    public T Value { get; private set; }
    public List<string> ErrorMessages { get; private set; }
    public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
     
    public Result(T value) => Value = value;
     
    public Result(List<string> errorMessages) => ErrorMessages = errorMessages;
}