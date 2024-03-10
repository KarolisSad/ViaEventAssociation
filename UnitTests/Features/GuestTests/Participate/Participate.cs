﻿using ViaEventAssociantion.Core.domain;
using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociantion.Core.domain.GuestProperties;
using ViaEventAssociantion.Core.domain.UserProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.Participate;

[TestFixture]
public class Participate
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
    public Task Participate_S1()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;

        //Act
        ResultBase response = createdEvent.Participate(guest);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsEmpty(response.ErrorMessages);
        Assert.That(createdEvent.Requests.First().Guest.Username, Is.EqualTo(guest.Username));

        return Task.CompletedTask;
    }

    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Cancelled)]
    [TestCase(EventStatus.Ready)]
    public Task Participate_F1(EventStatus eventStatus)
    {
        // Arrange
        createdEvent.Status = eventStatus;
        createdEvent.IsPublic = true;

        //Act
        ResultBase response = createdEvent.Participate(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Only active events can be joined.")
            )
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task Participate_F2()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.MaximumNumberOfGuests = 0;

        //Act
        ResultBase response = createdEvent.Participate(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message => message.Contains("The event is full."))
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task Participate_F3()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.StartTime = new DateTime(2023, 8, 8, 8, 8, 8, 8);

        //Act
        ResultBase response = createdEvent.Participate(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Only future events can be participated.")
            )
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task Participate_F4()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = false;

        //Act
        ResultBase response = createdEvent.Participate(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Only public events can be joined.")
            )
        );

        return Task.CompletedTask;
    }

    [Test]
    public Task Participate_F5()
    {
        // Arrange
        createdEvent.Status = EventStatus.Active;
        createdEvent.IsPublic = true;
        createdEvent.Participate(guest);
        createdEvent.Requests.First().Status = ParticipationStatus.Accepted;

        //Act
        ResultBase response = createdEvent.Participate(guest);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Guest has already joined the event.")
            )
        );

        return Task.CompletedTask;
    }
}
