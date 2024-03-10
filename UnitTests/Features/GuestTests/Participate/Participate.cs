using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.Participate;

[TestFixture]
public class Participate
{
    [Test]
    public Task Participate_S1()
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
    public Task Participate_S2()
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
}
