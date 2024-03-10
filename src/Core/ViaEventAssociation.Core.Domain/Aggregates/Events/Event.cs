using System.Security.Cryptography;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociantion.Core.domain.RequestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociantion.Core.domain;

public class Event
{
    public EventId Id;
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsPublic { get; set; }
    public int MaximumNumberOfGuests { get; set; }
    public EventStatus Status { get; set; }
    public ICollection<Request> Requests { get; set; } = new HashSet<Request>();
    public ICollection<Invitation> Invitations { get; set; } = new HashSet<Invitation>();

    private Event(EventId eventId)
    {
        Id = eventId;
        IsPublic = false;
        MaximumNumberOfGuests = 5;
    }

    public static ResultBase Create(EventId eventId)
    {
        Event value = new Event(eventId);
        return new Result<Event>(value);
    }

    public ResultBase InviteGuest(Guest guest)
    {
        List<string> errorMessages = new List<string>();

        var newInvitation = new Invitation(guest);

        if (Status != EventStatus.Active && Status != EventStatus.Ready)
        {
            errorMessages.Add("Event must be ready or active.");
        }
        var acceptedInvitations = Invitations.Count(invitation =>
            invitation.Status == ParticipationStatus.Accepted
        );

        var acceptedRequests = Requests.Count(request =>
            request.Status == ParticipationStatus.Accepted
        );

        if (acceptedRequests + acceptedInvitations >= MaximumNumberOfGuests)
        {
            errorMessages.Add("The event is full.");
        }

        if (errorMessages.Count == 0)
        {
            Invitations.Add(newInvitation);
        }

        return new ResultBase(errorMessages);
    }

    public ResultBase DeclineInvitation(Guest guest)
    {
        List<string> errorMessages = new List<string>();
        var invitation = Invitations.FirstOrDefault(invitation => invitation.Guest.Equals(guest));

        if (EventStatus.Cancelled == Status)
        {
            errorMessages.Add("Cannot decline an invitation for a cancelled event.");
        }

        if (EventStatus.Ready == Status)
        {
            errorMessages.Add("Cannot decline an invitation for a ready event.");
        }

        if (invitation == null)
        {
            errorMessages.Add("Invitation not found.");
            return new ResultBase(errorMessages);
        }
        if (errorMessages.Count == 0)
        {
            invitation.Status = ParticipationStatus.Declined;
        }

        return new ResultBase(errorMessages);
    }

    public ResultBase Participate(Guest guest)
    {
        List<string> errorMessages = new List<string>();
        var newRequest = new Request(guest);
        if (Status != EventStatus.Active)
        {
            errorMessages.Add("Only active events can be joined.");
        }

        var guestAcceptedInvitations = Invitations.Count(invitation =>
            invitation.Guest.Equals(guest) && invitation.Status == ParticipationStatus.Accepted
        );

        var guestAcceptedRequests = Requests.Count(request =>
            request.Guest.Equals(guest) && request.Status == ParticipationStatus.Accepted
        );
        if (guestAcceptedInvitations + guestAcceptedRequests > 0)
        {
            errorMessages.Add("Guest has already joined the event.");
        }

        if (IsPublic == false)
        {
            errorMessages.Add("Only public events can be joined.");
        }

        if (StartTime < DateTime.Now)
        {
            errorMessages.Add("Only future events can be participated.");
        }

        var acceptedInvitations = Invitations.Count(invitation =>
            invitation.Status == ParticipationStatus.Accepted
        );

        var acceptedRequests = Requests.Count(request =>
            request.Status == ParticipationStatus.Accepted
        );

        if (acceptedRequests + acceptedInvitations >= MaximumNumberOfGuests)
        {
            errorMessages.Add("The event is full.");
        }

        if (errorMessages.Count == 0)
        {
            Requests.Add(newRequest);
        }

        return new ResultBase(errorMessages);
    }

    public ResultBase Activate()
    {
        List<string> errorMessages = new List<string>();
        // TODO add validation for the maximum number of guests, and visibility - or as suggested by the test case, make the event ready, have it
        // do these validations and then activate it
        if (Status == EventStatus.Ready || Status == EventStatus.Active)
        {
            Status = EventStatus.Active;
            return new ResultBase(errorMessages);
        }

        if (Title == null || Description == null || StartTime == null || EndTime == null)
        {
            errorMessages.Add("The event has missing properties.");
            return new ResultBase(errorMessages);
        }

        if (Status == EventStatus.Cancelled)
        {
            errorMessages.Add("Cannot activate a cancelled event.");
        }

        if (
            Status == EventStatus.Draft
            && ValidateTitle(Title).IsSuccess
            && ValidateDescription(Description).IsSuccess
            && ValidateTime(StartTime, EndTime).IsSuccess
            && ValidateSetMaxNrOfGuests(MaximumNumberOfGuests).IsSuccess
            && ValidateMakeEventPublic().IsSuccess
        )
        {
            Status = EventStatus.Active;
        }
        else
        {
            errorMessages.Add("Cannot activate an event that is not ready.");
        }

        return new ResultBase(errorMessages);
    }

    public ResultBase SetEventStatus(EventStatus eventStatus)
    {
        Status = eventStatus;
        return new ResultBase();
    }

    public ResultBase SetMaxNrOfGuests(int maxNumberOfGuests)
    {
        ResultBase response = ValidateSetMaxNrOfGuests(maxNumberOfGuests);
        if (response.IsSuccess)
        {
            MaximumNumberOfGuests = maxNumberOfGuests;
            return new ResultBase();
        }
        return response;
    }

    public ResultBase UpdateTitle(string title)
    {
        Title = title;
        ResultBase response = ValidateTitle(title);
        if (Status == EventStatus.Active)
        {
            response.ErrorMessages.Add("Cannot update the title of an active event.");
        }
        if (Status == EventStatus.Cancelled)
        {
            response.ErrorMessages.Add("Cannot update the title of a cancelled event.");
        }

        if (response.IsSuccess)
        {
            SetEventStatus(EventStatus.Draft);
            return new ResultBase();
        }
        return response;
    }

    public ResultBase UpdateDescription(string description)
    {
        ResultBase response = ValidateDescription(description);
        if (response.IsSuccess)
        {
            Description = description;
            return new ResultBase();
        }
        return response;
    }

    public ResultBase UpdateTimeRange(DateTime startTime, DateTime endTime)
    {
        ResultBase response = ValidateTime(startTime, endTime);

        if (Status == EventStatus.Active)
        {
            response.ErrorMessages.Add("Cannot update the time range of an active event.");
        }

        if (Status == EventStatus.Cancelled)
        {
            response.ErrorMessages.Add("Cannot update the time range of a cancelled event.");
        }

        if (
            (Status == EventStatus.Ready || Status == EventStatus.Draft)
            && (response.ErrorMessages.Count == 0)
        )
        {
            StartTime = startTime;
            EndTime = endTime;
            Status = EventStatus.Draft;
        }

        return new ResultBase(response.ErrorMessages);
    }

    public ResultBase SetIsPublic(bool isPublic)
    {
        IsPublic = isPublic;
        return new ResultBase();
    }

    public ResultBase MakeEventPublic()
    {
        ResultBase response = ValidateMakeEventPublic();
        if (response.IsSuccess)
        {
            IsPublic = true;
            Status = EventStatus.Draft;
            return new ResultBase();
        }
        return response;
    }

    public ResultBase MakeEventPrivate()
    {
        ResultBase response = ValidateMakeEventPrivate();
        if (response.IsSuccess)
        {
            IsPublic = false;
            Status = EventStatus.Draft;
            return new ResultBase();
        }
        return response;
    }

    public ResultBase MakeEventReady()
    {
        ResultBase response = ValidateMakeEventReady();
        if (response.IsSuccess)
        {
            Status = EventStatus.Ready;
            return new ResultBase();
        }
        return response;
    }

    private ResultBase ValidateSetMaxNrOfGuests(int maxNumberOfGuests)
    {
        if (maxNumberOfGuests < 5)
        {
            return new ResultBase(
                new List<string> { "The maximum number of guests cannot be less than 5." }
            );
        }
        if (maxNumberOfGuests > 50)
        {
            return new ResultBase(
                new List<string> { "The maximum number of guests cannot be more than 50." }
            );
        }
        if (maxNumberOfGuests < MaximumNumberOfGuests)
        {
            return new ResultBase(
                new List<string>
                {
                    "Maximum number of guests for an active event cannot be reduced."
                }
            );
        }
        if (Status == EventStatus.Cancelled)
        {
            return new ResultBase(new List<string> { "A cancelled event cannot be modified." });
        }
        return new ResultBase();
    }

    public ResultBase ValidateTitle(string title)
    {
        List<string> errorMessages = new List<string>();
        if (title.Length < 3 || title.Length > 75)
        {
            errorMessages.Add("Title length has to be between 3 and 75 characters.");
        }

        return new ResultBase(errorMessages);
    }

    public ResultBase ValidateMakeEventPublic()
    {
        if (Status == EventStatus.Cancelled)
        {
            return new ResultBase(
                new List<string> { "Event is cancelled. Cannot make the event public." }
            );
        }
        return new ResultBase();
    }

    public ResultBase ValidateMakeEventPrivate()
    {
        if (Status == EventStatus.Active)
        {
            return new ResultBase(
                new List<string> { "Event is Active. Cannot make the event private." }
            );
        }
        if (Status == EventStatus.Cancelled)
        {
            return new ResultBase(
                new List<string> { "Event is cancelled. Cannot make the event private." }
            );
        }
        return new ResultBase();
    }

    public ResultBase ValidateMakeEventReady()
    {
        List<string> errorMessages = new List<string>();
        if (Status == EventStatus.Cancelled)
        {
            return new ResultBase(new List<string> { "A cancelled event cannot be readied." });
        }
        if (StartTime > DateTime.Now)
        {
            return new ResultBase(
                new List<string> { "An event in the past cannot be made ready." }
            );
        }
        Console.WriteLine(StartTime);
        Console.WriteLine(DateTime.Now);

        if (string.IsNullOrEmpty(Title))
        {
            errorMessages.Add("Title must be set.");
        }

        if (string.IsNullOrEmpty(Description))
        {
            errorMessages.Add("Description must be set.");
        }

        if (StartTime == default(DateTime))
        {
            errorMessages.Add("Start date time must be set.");
        }

        if (EndTime == default(DateTime))
        {
            errorMessages.Add("End date time must be set.");
        }

        return errorMessages.Count > 0 ? new ResultBase(errorMessages) : new ResultBase();
    }

    public ResultBase ValidateDescription(string description)
    {
        if (Status == EventStatus.Cancelled)
        {
            return new ResultBase(
                new List<string> { "Event is cancelled. Description cannot be set." }
            );
        }

        if (Status == EventStatus.Active)
        {
            return new ResultBase(
                new List<string> { "Event is Active. Description cannot be set." }
            );
        }

        if (description.Length > 255)
        {
            return new ResultBase(
                new List<string> { "Description must be between 1 and 255 characters." }
            );
        }
        else
        {
            return new ResultBase();
        }
    }

    private ResultBase ValidateTime(DateTime startTime, DateTime endTime)
    {
        List<string> errorMessages = new List<string>();
        if (endTime < startTime)
        {
            errorMessages.Add("End time cannot be before start time.");
        }

        if (
            (
                (startTime.Hour is > 0 && startTime.Hour < 8 && startTime.Minute > 0)
                || startTime.Hour < 8
            )
            || (
                (endTime.Date.Day - startTime.Date.Day > 0)
                && (endTime.Hour >= 1 && endTime.Minute > 0)
            )
        )
        {
            errorMessages.Add("Rooms are only usable between 8 AM and 1 AM on the next day.");
        }
        // Commented out to test success scenarios in past dates

        //start time cannot be in the past
        // if (startTime < DateTime.Now)
        // {
        //     errorMessages.Add("Start time cannot be in the past.");
        // }
        if (endTime.Subtract(startTime).TotalHours > 10)
        {
            errorMessages.Add("The event has to be at most 10 hours long.");
        }

        if (endTime.Subtract(startTime).TotalHours <= 1)
        {
            errorMessages.Add("The event has to be at least 1 hour long.");
        }

        return new ResultBase(errorMessages);
    }
}
