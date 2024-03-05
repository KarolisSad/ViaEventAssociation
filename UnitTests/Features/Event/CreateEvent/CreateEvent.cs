using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.CreateEvent;
[TestFixture]
public class CreateEvent
{
    [SetUp]
    public void Setup()
    {
      
    }


    [Test]
    public Task CreateEvent_S1()
    {
        //Arrange
        EventId eventId = new EventId(1);
        
        //Act
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        createdEvent.SetEventStatus(EventStatus.Draft);
        createdEvent.SetMaxNrOfGuests(5);
        
        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(createdEvent.MaximumNumberOfGuests, Is.EqualTo(5));
        Assert.That(createdEvent.Status, Is.EqualTo(EventStatus.Draft));
        return Task.CompletedTask;
    }
    
    [Test]
    public Task CreateEvent_S2()
    {
        //Arrange
        EventId eventId = new EventId(1);
        
        //Act
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        createdEvent.UpdateTitle("");
        
        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(createdEvent.Title, Is.EqualTo(""));
        return Task.CompletedTask;
    }
    
    [Test]
    public Task CreateEvent_S3()
    {
        //Arrange
        EventId eventId = new EventId(1);
        
        //Act
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        createdEvent.UpdateDescription("");
        
        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(createdEvent.Description, Is.EqualTo(""));
        return Task.CompletedTask;
    }
    
    [Test]
    public Task CreateEvent_S4()
    {
        //Arrange
        EventId eventId = new EventId(1);
        
        //Act
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        createdEvent.SetIsPublic(false);
        
        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(createdEvent.IsPublic, Is.EqualTo(false));
        return Task.CompletedTask;
    }
    
}
