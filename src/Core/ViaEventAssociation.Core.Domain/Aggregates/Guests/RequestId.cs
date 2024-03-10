namespace ViaEventAssociantion.Core.domain.RequestProperties;

public class RequestId
{
    public int Value;

    private static int counter = 0;

    public RequestId()
    {
        Value = ++counter;
    }
}
