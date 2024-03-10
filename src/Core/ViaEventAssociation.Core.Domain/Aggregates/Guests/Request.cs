using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.RequestProperties;

namespace ViaEventAssociantion.Core.domain;

public class Request
{
    public RequestId Id { get; set; }
    public ParticipationStatus Status { get; set; }

    public Guest Guest { get; set; }

    public Request(Guest guest)
    {
        Id = new RequestId();
        Status = ParticipationStatus.Pending;
        Guest = guest;
    }
}
