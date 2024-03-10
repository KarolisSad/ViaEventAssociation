namespace ViaEventAssociantion.Core.domain.InvitationProperties;

public class InvitationId
{
    private static int counter = 0;
    public int Value;

    public InvitationId()
    {
        Value = ++counter;
    }
}
