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
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public bool IsPublic { get; set; }
    public int MaximumNumberOfGuests { get; set; }
    public EventStatus Status { get; set; }
    public ICollection<Request> Requests { get; set; }
    public ICollection<Invitation> Invitations { get; set; }

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
    
    public ResultBase SetStartDateTime(DateTime startDateTime)
    {
        StartDateTime = startDateTime;
        return new ResultBase();
    }
    
    public ResultBase SetEndDateTime(DateTime endDateTime)
    {
        EndDateTime = endDateTime;
        return new ResultBase();
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
            return new ResultBase(new List<string> { "The maximum number of guests cannot be less than 5." });
        }
        if (maxNumberOfGuests < MaximumNumberOfGuests)
        {
            return new ResultBase(new List<string> { "Maximum number of guests for an active event cannot be reduced." });
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
            return new ResultBase(new List<string> { "Event is cancelled. Cannot make the event public." });
        }
        return new ResultBase();
    }
    
    public ResultBase ValidateMakeEventPrivate()
    {
        if (Status == EventStatus.Active)
        {
            return new ResultBase(new List<string> { "Event is Active. Cannot make the event private." });
        }
        if (Status == EventStatus.Cancelled)
        {
            return new ResultBase(new List<string> { "Event is cancelled. Cannot make the event private." });
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
        if (string.IsNullOrEmpty(Title))
        {
            errorMessages.Add("Title must be set.");
        }

        if (string.IsNullOrEmpty(Description))
        {
            errorMessages.Add("Description must be set.");
        }

        if (StartDateTime == default(DateTime))
        {
            errorMessages.Add("Start date time must be set.");
        }

        if (EndDateTime == default(DateTime))
        {
            errorMessages.Add("End date time must be set.");
        }

        return errorMessages.Count > 0 ? new ResultBase(errorMessages) : new ResultBase();
    }


    public ResultBase ValidateDescription(string description)
    {
        if (Status == EventStatus.Cancelled)
        {
            return new ResultBase(new List<string> { "Event is cancelled. Description cannot be set." });
        }
        
        if (Status == EventStatus.Active)
        {
            return new ResultBase(new List<string> { "Event is Active. Description cannot be set." });
        }

        if (description.Length > 255)
        {
            return new ResultBase(new List<string> { "Description must be between 1 and 255 characters." });
        }
        else
        {
            return new ResultBase();
        }
    }

}