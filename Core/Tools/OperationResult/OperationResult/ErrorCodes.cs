namespace ClassLibrary1;

public class ErrorMessage
{
    public class User
    {
        public const string InvalidEmailAddress = "USR_001";
        public const string InvalidEmailAddressMessage = "Email address is invalid.";

        public const string InvalidPassword = "USR_002";
        public const string InvalidPasswordMessage = "Password is invalid.";

        public const string InvalidUsername = "USR_003";
        public const string InvalidUsernameMessage = "Username is invalid.";
    }

    public class Event
    {
        public const string InvalidDescription = "EVT_001";
        public const string InvalidDescriptionMessage = "The entered description is invalid.";

        public const string InvalidTitle = "EVT_002";
        public const string InvalidTitleMessage = "The entered title is invalid.";

        public const string LocationIsReserved = "EVT_003";
        public const string LocationIsReservedMessage = "The selected location is already reserved.";

        public const string StartTimeIsAfterEndTime = "EVT_004";
        public const string StartTimeIsAfterEndTimeMessage = "The start time of the event is after the end time.";

        public const string MaximumNumberOfGuestsReached = "EVT_005";
        public const string MaximumNumberOfGuestsReachedMessage = "Maximum number of guests has been reached.";
    }

    public class Location
    {
        public const string InvalidType = "LOC_001";
        public const string InvalidTypeMessage = "The entered type is invalid.";

        public const string InvalidCapacity = "LOC_002";
        public const string InvalidCapacityMessage = "The entered capacity is invalid.";

        public const string InvalidName = "LOC_003";
        public const string InvalidNameMessage = "The entered name is invalid.";
    }
}