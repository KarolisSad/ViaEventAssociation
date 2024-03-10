using ViaEventAssociantion.Core.domain.UserProperties;

namespace ViaEventAssociantion.Core.domain;

public class User
{
    public UserId Id { get; set; }
    public Username Username { get; set; }
    public Password Password { get; set; }
}
