﻿using ViaEventAssociantion.Core.domain.Enums;
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
        MaximumNumberOfGuests = maxNumberOfGuests;
        return new ResultBase();
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
        Description = description;
        return new ResultBase();
    }

    public ResultBase SetIsPublic(bool isPublic)
    {
        IsPublic = isPublic;
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
    
}