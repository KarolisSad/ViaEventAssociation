using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.Activate;

[TestFixture]
public class Activate
{
    [Test]
    public Task Activate_S1()
    {
        //Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;
        createdEvent.StartTime = new DateTime(2026, 8, 8, 16, 8, 8, 8);
        createdEvent.EndTime = new DateTime(2026, 8, 8, 20, 8, 8, 8);
        createdEvent.Title = "Scary Movie Night!";
        createdEvent.Description = "A night of horror and fun!";

        //Act
        ResultBase response = createdEvent.Activate();

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsEmpty(response.ErrorMessages);
        Assert.That(createdEvent.Status, Is.EqualTo(EventStatus.Active));

        return Task.CompletedTask;
    }

    [Test]
    public Task Activate_S2()
    {
        //Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;
        createdEvent.Status = EventStatus.Ready;

        //Act
        ResultBase response = createdEvent.Activate();

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsEmpty(response.ErrorMessages);
        Assert.That(createdEvent.Status, Is.EqualTo(EventStatus.Active));

        return Task.CompletedTask;
    }

    [Test]
    public Task Activate_S3()
    {
        //Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;
        createdEvent.Status = EventStatus.Active;

        //Act
        ResultBase response = createdEvent.Activate();

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsEmpty(response.ErrorMessages);
        Assert.That(createdEvent.Status, Is.EqualTo(EventStatus.Active));

        return Task.CompletedTask;
    }

    [Test]
    public Task Activate_F1()
    {
        //Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;
        createdEvent.Status = EventStatus.Draft;

        //Act
        ResultBase response = createdEvent.Activate();

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);

        return Task.CompletedTask;
    }

    [Test]
    public Task Activate_F2()
    {
        //Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;
        createdEvent.Status = EventStatus.Cancelled;
        createdEvent.StartTime = new DateTime(2026, 8, 8, 16, 8, 8, 8);
        createdEvent.EndTime = new DateTime(2026, 8, 8, 20, 8, 8, 8);
        createdEvent.Title = "Scary Movie Night!";
        createdEvent.Description = "A night of horror and fun!";

        //Act
        ResultBase response = createdEvent.Activate();

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotEmpty(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Cannot activate a cancelled event.")
            )
        );
        return Task.CompletedTask;
    }
}
