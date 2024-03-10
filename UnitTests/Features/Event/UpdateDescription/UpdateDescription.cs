using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateDescription;

public class UpdateDescription
{
    [Test]
    public void UpdateDescription_S1()
    {
        //Arrange
        EventId eventId = new EventId(1);
        var description =
            "Nullam tempor lacus nisl, eget tempus\nquam maximus malesuada. ";
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        
        //Act
        createdEvent.UpdateDescription(description);
        
        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(createdEvent.Description, Is.EqualTo(description));
    }
    
    [Test]
    public void UpdateDescription_S2()
    {
        //Arrange
        EventId eventId = new EventId(1);
        var description =
            "";
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        createdEvent.SetEventStatus(EventStatus.Draft);
        
        //Act
        createdEvent.UpdateDescription(description);
        
        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(createdEvent.Description, Is.EqualTo(description));
    }
    
    [Test]
    public void UpdateDescription_S3()
    {
        //Arrange
        EventId eventId = new EventId(1);
        var description = "";
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        createdEvent.SetEventStatus(EventStatus.Ready);
        
        //Act
        createdEvent.UpdateDescription(description);
        
        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(createdEvent.Description, Is.EqualTo(description));
    }
    
    [Test]
    public void UpdateDescription_F1()
    {
        //Arrange
        EventId eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        var description =
            "Nullam tempor lacus nisl, eget tempus\nquam maximus malesuada. Morbi fauxcibus\nsed neque vitae euismod. Vestibulum\nnon purus vel justo ornare vulputate.\nIn a interdum enim. Maecenas sed\nsodales elit, sit amet venenatis orci.\nSuspendisse potenti. Sed pulvinar\nturpis ut euismod varius. Nullam\nturpis tellus, tincidunt ut quam\nconvallis, auctor mollis nunc. Aliquam\nerat volutpat. Nullam tempor lacus nisl, eget tempus\nquam maximus malesuada. Morbi faucibus\nsed neque vitae euismod. Vestibulum\nnon purus vel justo ornare vulputate.\nIn a interdum enim. Maecenas sed\nsodales elit, sit amet venenatis orci.\nSuspendisse potenti. Sed pulvinar\nturpis ut euismod varius. Nullam\nturpis tellus, tincidunt ut quam\nconvallis, auctor mollis nunc. Aliquam\nerat volutpat. Nullam tempor lacus nisl, eget tempus\nquam maximus malesuada. Morbi faucibus\nsed neque vitae euismod. Vestibulum\nnon purus vel justo ornare vulputate.\nIn a interdum enim. Maecenas sed\nsodales elit, sit amet venenatis orci.\nSuspendisse potenti. Sed pulvinar\nturpis ut euismod varius. Nullam\nturpis tellus, tincidunt ut quam\nconvallis, auctor mollis nunc. Aliquam\nerat volutpat. ";
        createdEvent.SetEventStatus(EventStatus.Draft);
        
        //Act
        ResultBase resultBase = createdEvent.UpdateDescription(description);
        
        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(createdEvent.Description, Is.EqualTo(null));
        Assert.IsNotNull(resultBase.ErrorMessages);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(resultBase.ErrorMessages[0], Is.EqualTo("Description must be between 1 and 255 characters."));
    }
    
    [Test]
    public void UpdateDescription_F2()
    {
        //Arrange
        EventId eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        var description =
            "Great event :)";
        createdEvent.SetEventStatus(EventStatus.Cancelled);
        
        //Act
        ResultBase resultBase = createdEvent.UpdateDescription(description);
        
        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(createdEvent.Description, Is.EqualTo(null));
        Assert.IsNotNull(resultBase.ErrorMessages);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(resultBase.ErrorMessages[0], Is.EqualTo("Event is cancelled. Description cannot be set."));
    }
    
    [Test]
    public void UpdateDescription_F3()
    {
        //Arrange
        EventId eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        var description =
            "Great event :)";
        createdEvent.SetEventStatus(EventStatus.Active);
        
        //Act
        ResultBase resultBase = createdEvent.UpdateDescription(description);
        
        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(createdEvent.Description, Is.EqualTo(null));
        Assert.IsNotNull(resultBase.ErrorMessages);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(resultBase.ErrorMessages[0], Is.EqualTo("Event is Active. Description cannot be set."));
    }
    
    
}