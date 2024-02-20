namespace ClassLibrary1;

public class ErrorCodes
{ 
    public class User
    {
        public const string InvalidEmailAddress = "Email address is invalid."
        public const string InvalidPassword = "Password is invalid."
        public const string InvalidUsername = "Username is invalid."
    }
    public class Event
    {
        public const string InvalidDescription = "The entered description is invalid."
        public const string InvalidTitle = "The entered title is invalid."
        public const string LocationIsReserved = "The selected location is already reserved."
        public const string StartTimeIsAfterEndTime = "The start time of the event is after the end time."
        public const string MaximumNumberOfGuestsReached = "Maximum number of guests has been reached."
    }

    public class Location
    {
        public const string InvalidType = "The entered type is invalid."
        public const string InvalidCapacity = "The entered capacity is invalid."
        public const string InvalidName = "The entered name is invalid."
    }
}