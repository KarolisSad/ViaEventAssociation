using System.Text.RegularExpressions;
using ViaEventAssociantion.Core.domain;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Users;

public class Anonymous
{
    private Anonymous()
    {
    }

    public static ResultBase Create()
    {
        Anonymous value = new Anonymous();
        return new Result<Anonymous>(value);
    }
    
    public ResultBase RegisterUserAccount(string viaMail, string firstName, string lastName, string password)
    {
        ResultBase createdFirstName = FirstName.Create(firstName);
        if (!createdFirstName.IsSuccess)
        {
            return new ResultBase(
                createdFirstName.ErrorMessages
            );
        }
        FirstName validFirstName = ((Result<FirstName>)createdFirstName).Values;
        
        ResultBase createdLastName = LastName.Create(lastName);
        if (!createdLastName.IsSuccess)
        {
            return new ResultBase(
                createdLastName.ErrorMessages
            );
        }
        LastName validLastName = ((Result<LastName>)createdLastName).Values;
            
        ResultBase myMail = Mail.Create(viaMail);
        if (!myMail.IsSuccess)
        {
            return new ResultBase(
                myMail.ErrorMessages
            );
        }
        Mail xMail = ((Result<Mail>)myMail).Values;
        
        ResultBase registeredGuestBase = Guest.Create(xMail, validFirstName, validLastName, password);
        if (!registeredGuestBase.IsSuccess)
        {
            return new ResultBase(
                registeredGuestBase.ErrorMessages
            );
        }
        Guest registeredGuest = ((Result<Guest>)registeredGuestBase).Values;
        return new Result<Guest>(registeredGuest);
    }
    
}