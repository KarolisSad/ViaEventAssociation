using ViaEventAssociantion.Core.domain;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Users;

public class Anonymous
{
    public static ResultBase RegisterUserAccount(
        int id,
        string username,
        string password,
        string email
    )
    {
        Username username1 = new Username(username);
        Password password1 = new Password(password);
        UserId userId = new UserId(id);

        ResultBase userMail = UserMail.Create(email);
        if (!userMail.IsSuccess)
        {
            return new ResultBase(userMail.ErrorMessages);
        }

        UserMail userMail1 = ((Result<UserMail>)userMail).Values;

        Guest guest = new Guest(userId, username1, password1, userMail1);
        return new Result<Guest>(guest);
    }
}

