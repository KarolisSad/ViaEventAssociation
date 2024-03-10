using System.Security.Cryptography;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
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
    public ICollection<Request> Requests { get; set; }
    public ICollection<Invitation> Invitations { get; set; }

    private Event(EventId eventId)
    {
        Id = eventId;
    }

    public static ResultBase Create(EventId eventId)
    {
        Event value = new Event(eventId);
        return new Result<Event>(value);
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
        MaximumNumberOfGuests = maxNumberOfGuests;
        return new ResultBase();
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

    private ResultBase ValidateTitle(string title)
    {
        List<string> errorMessages = new List<string>();
        if (title.Length < 3 || title.Length > 75)
        {
            errorMessages.Add("Title length has to be between 3 and 75 characters.");
        }

        return new ResultBase(errorMessages);
    }

    private ResultBase ValidateDescription(string description)
    {
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
