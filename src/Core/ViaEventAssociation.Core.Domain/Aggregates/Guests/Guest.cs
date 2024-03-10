using System;
using System.Text.RegularExpressions;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Domain.Aggregates.Users;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociantion.Core.domain
{
    public class Guest : User
    {
        public Mail Email { get; private set; }
        public FirstName UserFirstName;
        public LastName UserLastName;
        public string Username;
        public string Password;

        private Guest(Mail viaMail, FirstName firstName, LastName lastName, string password)
        {
            Email = viaMail;
            UserFirstName = firstName;
            UserLastName = lastName;
            Password = password;
        }

        public static ResultBase Create(Mail viaMail, FirstName firstName, LastName lastName, string password)
        {
            Guest value = new Guest(viaMail,firstName,lastName,password);
            return new Result<Guest>(value);
        }
    }
}
