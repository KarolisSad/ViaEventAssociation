using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakePublic;

public class MakePublic
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
        var description = "Great event :)";
        createdEvent.SetEventStatus(EventStatus.Draft);
        
        //Act
        ResultBase resultBase = createdEvent.MakeEventPublic();
        
        //Assert
        Assert.IsTrue(resultBase.IsSuccess);
        Assert.That(createdEvent.IsPublic, Is.EqualTo(true));
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void UpdateDescription_F1()
    {
        //Arrange
        var description = "Great event :)";
        createdEvent.SetEventStatus(EventStatus.Cancelled);
        
        //Act
        ResultBase resultBase = createdEvent.MakeEventPublic();
        
        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(resultBase.ErrorMessages[0], Is.EqualTo( "Event is cancelled. Cannot make the event public."));
    }
}
