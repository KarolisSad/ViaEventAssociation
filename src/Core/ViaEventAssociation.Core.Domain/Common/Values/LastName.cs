using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Users;

public class LastName
{
    public string Name { get; set; }
    
    private LastName(string lastName)
    {
        Name = lastName;
    }

    public static ResultBase Create(string lastName)
    {
        ResultBase result = Validate(lastName);
        if (result.IsSuccess)
        {
            LastName fName = new LastName(CapitalizeFirstLetter(lastName));
            return new Result<LastName>(fName);
        }
        return new ResultBase(
            result.ErrorMessages
        );
    }

    public static ResultBase Validate(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 25 || !Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
        {
            return new ResultBase(
                new List<string> { "Last name must be between 2 and 25 characters long and contain only letters." }
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