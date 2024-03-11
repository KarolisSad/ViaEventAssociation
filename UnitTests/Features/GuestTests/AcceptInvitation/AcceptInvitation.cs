using ViaEventAssociantion.Core.domain;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.AcceptInvitation;

public class AcceptInvitation
{
    private ViaEventAssociantion.Core.domain.Event createdEvent;
    private Guest guest;
    private Guest guest2;
    private Guest guest3;
    private Guest guest4;
    private Guest guest5;
    private Guest guest6;
    private Guest guest7;

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

        var userId2 = new UserId(2);
        var username2 = new Username("BennyBenson");
        var password2 = new Password("SecurePass456!");
        var userMail2 = new UserMail("andero@via.dk");
        guest2 = new Guest(userId2, username2, password2, userMail2);

        var userId3 = new UserId(3);
        var username3 = new Username("CharlieCharles");
        var password3 = new Password("Pass1234!");
        var userMail3 = new UserMail("charlie@example.com");
        guest3 = new Guest(userId3, username3, password3, userMail3);

        var userId4 = new UserId(4);
        var username4 = new Username("DavidDavidson");
        var password4 = new Password("MyPassw0rd!");
        var userMail4 = new UserMail("david@example.com");
        guest4 = new Guest(userId4, username4, password4, userMail4);

        var userId5 = new UserId(5);
        var username5 = new Username("EmilyEmilson");
        var password5 = new Password("StrongPass789!");
        var userMail5 = new UserMail("emily@example.com");
        guest5 = new Guest(userId5, username5, password5, userMail5);

        var userId6 = new UserId(6);
        var username6 = new Username("FrankFranklin");
        var password6 = new Password("Passw0rd456!");
        var userMail6 = new UserMail("frank@example.com");
        guest6 = new Guest(userId6, username6, password6, userMail6);

        var userId7 = new UserId(6);
        var username7 = new Username("FrankFranklio");
        var password7 = new Password("Passw0rd452!");
        var userMail7 = new UserMail("frano@example.com");
        guest7 = new Guest(userId6, username6, password6, userMail6);
    }

    [Test]
    public void AcceptInvitation_S1()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.InviteGuest(guest);

        //Act
        ResultBase response = createdEvent.AcceptInvitation(guest);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsEmpty(response.ErrorMessages);
        Assert.That(
            createdEvent.Invitations.First().Status,
            Is.EqualTo(ParticipationStatus.Accepted)
        );
    }

    [Test]
    public void AcceptInvitation_F1()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;

        //Act
        ResultBase response = createdEvent.AcceptInvitation(guest);

        //Assert
        Assert.IsTrue(!response.IsSuccess);
        Assert.That(response.ErrorMessages[0], Is.EqualTo("Invitation not found."));
    }

    [Test]
    public void AcceptInvitation_F2()
    {
        // Arrange
        createdEvent.SetMaxNrOfGuests(5);
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.InviteGuest(guest);
        createdEvent.InviteGuest(guest2);
        createdEvent.InviteGuest(guest3);
        createdEvent.InviteGuest(guest4);
        createdEvent.InviteGuest(guest5);
        createdEvent.InviteGuest(guest6);

        createdEvent.AcceptInvitation(guest);
        createdEvent.AcceptInvitation(guest2);
        createdEvent.AcceptInvitation(guest3);
        createdEvent.AcceptInvitation(guest4);
        createdEvent.AcceptInvitation(guest5);

        //Act
        ResultBase response = createdEvent.AcceptInvitation(guest6);

        //Assert
        Assert.IsTrue(!response.IsSuccess);
        Assert.That(response.ErrorMessages[0], Is.EqualTo("The event is full."));
    }

    [Test]
    public void AcceptInvitation_F3()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.InviteGuest(guest);
        createdEvent.Status = EventStatus.Cancelled;

        //Act
        ResultBase response = createdEvent.AcceptInvitation(guest);

        //Assert
        Assert.IsTrue(!response.IsSuccess);
        Assert.That(response.ErrorMessages[0], Is.EqualTo("Cancelled events cannot be joined."));
    }

    [Test]
    public void AcceptInvitation_F4()
    {
        // Arrange
        createdEvent.Status = EventStatus.Ready;
        createdEvent.IsPublic = true;
        createdEvent.InviteGuest(guest);

        //Act
        ResultBase response = createdEvent.AcceptInvitation(guest);

        //Assert
        Assert.IsTrue(!response.IsSuccess);
        Assert.That(response.ErrorMessages[0], Is.EqualTo("The event cannot yet be joined."));
    }
}
