using ViaEventAssociantion.Core.domain;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.DeclineInvitation;

[TestFixture]
public class DeclineInvitation
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
    public Task DeclineInvitation_S1()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.InviteGuest(guest);

        //Act
        ResultBase response = createdEvent.DeclineInvitation(guest);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsEmpty(response.ErrorMessages);
        Assert.That(
            createdEvent.Invitations.First().Status,
            Is.EqualTo(ParticipationStatus.Declined)
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task DeclineInvitation_S2()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.InviteGuest(guest);
        createdEvent.Invitations.First().Status = ParticipationStatus.Accepted;

        //Act
        ResultBase response = createdEvent.DeclineInvitation(guest);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsEmpty(response.ErrorMessages);
        Assert.That(
            createdEvent.Invitations.First().Status,
            Is.EqualTo(ParticipationStatus.Declined)
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task DeclineInvitation_F1()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;

        //Act
        ResultBase response = createdEvent.DeclineInvitation(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message => message.Contains("Invitation not found."))
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task DeclineInvitation_F2()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.InviteGuest(guest);

        createdEvent.Status = EventStatus.Cancelled;

        //Act
        ResultBase response = createdEvent.DeclineInvitation(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Cannot decline an invitation for a cancelled event.")
            )
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task DeclineInvitation_F3()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.InviteGuest(guest);

        createdEvent.Status = EventStatus.Ready;

        //Act
        ResultBase response = createdEvent.DeclineInvitation(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Cannot decline an invitation for a ready event.")
            )
        );

        return Task.CompletedTask;
    }
}
