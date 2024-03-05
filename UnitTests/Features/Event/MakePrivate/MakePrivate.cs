using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakePrivate;

public class MakePrivate
{
    private ViaEventAssociantion.Core.domain.Event createdEvent;
    
    [SetUp]
    public void Setup()
    {
        EventId eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
    }
    
    [Test]
    public void UpdateDescription_S1()
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Ready);
        createdEvent.MakeEventPrivate();
        
        //Act
        ResultBase resultBase = createdEvent.MakeEventPrivate();
        
        //Assert
        Assert.IsTrue(resultBase.IsSuccess);
        Assert.That(createdEvent.IsPublic, Is.EqualTo(false));
        Assert.That(createdEvent.Status, Is.EqualTo(EventStatus.Draft));
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void UpdateDescription_S2()
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Ready);
        createdEvent.MakeEventPublic();
        
        //Act
        ResultBase resultBase = createdEvent.MakeEventPrivate();
        
        //Assert
        Assert.IsTrue(resultBase.IsSuccess);
        Assert.That(createdEvent.IsPublic, Is.EqualTo(false));
        Assert.That(createdEvent.Status, Is.EqualTo(EventStatus.Draft));
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void UpdateDescription_F1()
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Active);
        
        //Act
        ResultBase resultBase = createdEvent.MakeEventPrivate();
        
        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(resultBase.ErrorMessages[0], Is.EqualTo( "Event is Active. Cannot make the event private."));
    }
    
    [Test]
    public void UpdateDescription_F2()
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Cancelled);
        
        //Act
        ResultBase resultBase = createdEvent.MakeEventPrivate();
        
        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(resultBase.ErrorMessages[0], Is.EqualTo( "Event is cancelled. Cannot make the event private."));
    }
}