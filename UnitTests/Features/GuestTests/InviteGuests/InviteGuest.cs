using ViaEventAssociantion.Core.domain;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.InviteGuests;

[TestFixture]
public class InviteGuest
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
    public Task InviteGuest_S1()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;

        //Act
        ResultBase response = createdEvent.InviteGuest(guest);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsEmpty(response.ErrorMessages);
        Assert.That(createdEvent.Invitations.First().Guest.Username, Is.EqualTo(guest.Username));

        return Task.CompletedTask;
    }

    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Cancelled)]
    public Task InviteGuest_F1(EventStatus eventStatus)
    {
        // Arrange
        createdEvent.Status = eventStatus;
        createdEvent.IsPublic = true;

        //Act
        ResultBase response = createdEvent.InviteGuest(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Event must be ready or active.")
            )
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task InviteGuest_F2()
    {
        // Arrange

        var userId2 = new UserId(2);
        var username2 = new Username("NewUser");
        var password2 = new Password("Password123!");
        var userMail2 = new UserMail("newUser@via.dk");
        var guest2 = new Guest(userId2, username2, password2, userMail2);

        var userId3 = new UserId(3);
        var username3 = new Username("EvenNewerUser");
        var password3 = new Password("Password123!");
        var userMail3 = new UserMail("newNewUser@via.dk");
        var guest3 = new Guest(userId3, username3, password3, userMail3);

        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.MaximumNumberOfGuests = 2;
        createdEvent.InviteGuest(guest2);
        createdEvent.Invitations.First().Status = ParticipationStatus.Accepted;

        createdEvent.Participate(guest3);
        createdEvent.Requests.First().Status = ParticipationStatus.Accepted;

        //Act
        ResultBase response = createdEvent.InviteGuest(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsTrue(
            response.ErrorMessages.Any(message => message.Contains("The event is full."))
        );

        return Task.CompletedTask;
    }
}
