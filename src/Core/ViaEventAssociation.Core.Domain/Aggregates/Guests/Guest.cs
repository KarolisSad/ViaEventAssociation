using System;
using System.Text.RegularExpressions;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociantion.Core.domain
{
    public class Guest : User
    {
        public UserMail Email { get; private set; }

        public ResultBase Register(string email, string firstName, string lastName)
        {
            if (!ValidateEmail(email))
            {
                return new ResultBase(new List<string> { "Invalid email format." });
            }

            if (!ValidateName(firstName) || !ValidateName(lastName))
            {
                return new ResultBase(new List<string> { "First name and last name must be between 2 and 25 characters and consist only of letters." });
            }

            firstName = char.ToUpper(firstName[0]) + firstName.Substring(1).ToLower();
            lastName = char.ToUpper(lastName[0]) + lastName.Substring(1).ToLower();

            
            // Create User 
            // Not implented yet

            return new ResultBase();
        }

        private bool ValidateEmail(string email)
        {
            // Ensure email ends with "@via.dk"
            if (!email.EndsWith("@via.dk", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Validate email format
            Regex regex = new Regex(@"^[a-zA-Z0-9]{3,4}\@[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$");
            return regex.IsMatch(email);
        }

        private bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 2 && name.Length <= 25 && Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }
    }
}
