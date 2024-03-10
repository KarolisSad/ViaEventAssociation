using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Users;

public class FirstName
{
    public string Name { get; set; }
    
    private FirstName(string firstName)
    {
        Name = firstName;
    }

    public static ResultBase Create(string firstName)
    {
        ResultBase result = Validate(firstName);
        if (result.IsSuccess)
        {
            FirstName fName = new FirstName(CapitalizeFirstLetter(firstName));
            return new Result<FirstName>(fName);
        }
        return new ResultBase(
            result.ErrorMessages
        );
    }

    public static ResultBase Validate(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 25 || !Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
        {
            return new ResultBase(
                new List<string> { "First name must be between 2 and 25 characters long and contain only letters." }
            );
        }
        return new ResultBase();
    }
    
    private static string CapitalizeFirstLetter(string input)
    {
        char firstChar = char.ToUpper(input[0]);
        string restOfWord = input.Substring(1).ToLower();
        return firstChar + restOfWord;
    }
    
    public string toString()
    {
        return Name.ToString();
    }
}