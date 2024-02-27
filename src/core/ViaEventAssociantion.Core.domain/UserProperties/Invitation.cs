using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.InvitationProperties;

namespace ViaEventAssociantion.Core.domain.UserProperties;

public class Invitation
{
    public InvitationId Id { get; set; }
    public ParticipationStatus Status { get; set; }
}