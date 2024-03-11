using System;
using System.Text.RegularExpressions;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociantion.Core.domain
{
    public class Guest : User
    {
        public UserMail Email { get; private set; }

        public Guest(UserId id, Username username, Password password, UserMail mail)
            : base(id, username, password)
        {
            Email = mail;
        }
        
    }
}
