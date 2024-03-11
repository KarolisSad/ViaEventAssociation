using ViaEventAssociantion.Core.domain;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.CancelParticipation;

public class CancelParticipation
{
    
    private ViaEventAssociantion.Core.domain.Event createdEvent;
    private Guest guest;
    
    [SetUp]
    public void Setup()
    {
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        createdEvent.StartTime = new DateTime(2026, 8, 8, 16, 8, 8, 8);
        createdEvent.EndTime = new DateTime(2026, 8, 8, 20, 8, 8, 8);
        createdEvent.Title = "Scary Movie Night!";
        createdEvent.Description = "A night of horror and fun!";

        var userId = new UserId(1);
        var username = new Username("AndersAndersen");
        var password = new Password("Password123!");
        var userMail = new UserMail("anders@via.dk");
        guest = new Guest(userId, username, password, userMail);
    }
    
    
    [Test]
    public void CancelParticipation_S1()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        ResultBase response = createdEvent.Participate(guest);

        //Act
        ResultBase response1 = createdEvent.CancelParticipation(guest);

        //Assert
        
        Assert.IsTrue(response1.IsSuccess);
        Assert.IsEmpty(response1.ErrorMessages);
        Assert.That(createdEvent.Requests.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void CancelParticipation_S2()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;

        //Act
        ResultBase response1 = createdEvent.CancelParticipation(guest);

        //Assert
        
        Assert.IsTrue(response1.IsSuccess);
        Assert.IsEmpty(response1.ErrorMessages);
        Assert.That(createdEvent.Requests.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void CancelParticipation_F1()
    {
        // Arrange
        DateTime currentTime = DateTime.Now;
        DateTime startTime = currentTime.AddHours(-1);
        DateTime endTime = currentTime.AddHours(2);
        createdEvent.UpdateTimeRange(startTime, endTime);
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        ResultBase response = createdEvent.Participate(guest);

        //Act
        ResultBase response1 = createdEvent.CancelParticipation(guest);

        //Assert
        
        Assert.IsTrue(!response1.IsSuccess);
        Assert.That(response1.ErrorMessages[0], Is.EqualTo("You cannot cancel your participation for past or ongoing events."));
    }
}

