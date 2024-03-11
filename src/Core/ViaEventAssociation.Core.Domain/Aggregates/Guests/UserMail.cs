using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociantion.Core.domain.GuestProperties;

public class UserMail
{
    public string EMail { get; set; }

    public UserMail(string mail)
    {
        EMail = mail;
    }

    public static ResultBase Create(string mail)
    {
        ResultBase result = Validate(mail);
        if (result.IsSuccess)
        {
            UserMail mEmail = new UserMail(mail);
            return new Result<UserMail>(mEmail);
        }

        return new ResultBase(
            result.ErrorMessages
        );
    }

    public static ResultBase Validate(string email)
    {
        List<string> errorMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(email) || !email.EndsWith("@via.dk") ||
            !Regex.IsMatch(email, @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$"))
        {
            Console.WriteLine("current email");
            Console.WriteLine(email);
            errorMessages.Add("Email must be in the correct format (e.g., example@via.dk).");
        }
        else
        {
            string[] emailParts = email.Split('@');
            string emailText1 = emailParts[0];
            if (emailText1.Length < 3 || emailText1.Length > 6 || (!Regex.IsMatch(emailText1, @"^[a-zA-Z]{3,4}$") &&
                                                                   !Regex.IsMatch(emailText1, @"^\d{6}$")))
            {
                errorMessages.Add(
                    "The text before '@' in the email must be between 3 and 6 characters long and match either 3 or 4 uppercase/lowercase English letters or 6 digits.");
            }
        }

        return new ResultBase(errorMessages);
    }
}
