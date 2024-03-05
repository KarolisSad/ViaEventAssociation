using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateTitle;

[TestFixture]
public class UpdateTitle
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase ("Scary Movie Night!")]
    [TestCase ("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    public Task UpdateTitle_S1(string title)
    { 
        //Arrange 
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        
        //Act 
        createdEvent.UpdateTitle(title);

        //Assert
        Assert.That(createdEvent.Title, Is.EqualTo(title));
        return Task.CompletedTask;
    }

    [Test]
    [TestCase("Scary Movie Night!")]
    [TestCase("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    public Task UpdateTitle_S2(string title)
    {
        //Arrange 
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        createdEvent.SetEventStatus(EventStatus.Ready);
        
        //Act 
        createdEvent.UpdateTitle(title);
        
        //Assert
        Assert.That(createdEvent.Title, Is.EqualTo(title));
        Assert.That(createdEvent.Status, Is.EqualTo(EventStatus.Draft));
        return Task.CompletedTask;
    }

    [Test]
    public Task 
UpdateTitle_F1()
    {
        //Arrange 
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        var title = "";
        
        //Act 
        ResultBase response= createdEvent.UpdateTitle(title);
        
        //Assert
        Assert.IsFalse(response.IsSuccess); 
        Assert.IsNotNull(response.ErrorMessages);
        Assert.That(response.ErrorMessages, Has.Count.EqualTo(1));
        Assert.That(response.ErrorMessages[0], Is.EqualTo("Title length has to be between 3 and 75 characters."));
        return Task.CompletedTask;
    }
    [Test]
    [TestCase ("XY")]
    [TestCase ("a")]
   public Task UpdateTitle_F2(string title)
    {
        //Arrange 
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
     
        
        //Act 
        ResultBase response= createdEvent.UpdateTitle(title);
        
        //Assert
        Assert.IsFalse(response.IsSuccess); 
        Assert.IsNotNull(response.ErrorMessages);
        Assert.That(response.ErrorMessages, Has.Count.EqualTo(1));
        Assert.That(response.ErrorMessages[0], Is.EqualTo("Title length has to be between 3 and 75 characters."));
        return Task.CompletedTask;
    }
   [Test]
    public Task UpdateTitle_F3()
    {
        //Arrange 
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        var title =
            "75+charactersaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        
        //Act 
        ResultBase response= createdEvent.UpdateTitle(title);
        
        //Assert
        Assert.IsFalse(response.IsSuccess); 
        Assert.IsNotNull(response.ErrorMessages);
        Assert.That(response.ErrorMessages, Has.Count.EqualTo(1));
        Assert.That(response.ErrorMessages[0], Is.EqualTo("Title length has to be between 3 and 75 characters."));
        return Task.CompletedTask;
    }
    
    [Test]
    public Task UpdateTitle_F4()
    {
        //Arrange 
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;
        string title = "";
        
        //Act 
        ResultBase response= createdEvent.UpdateTitle(title);
        
        //Assert
        Assert.IsFalse(response.IsSuccess); 
        Assert.IsNotNull(response.ErrorMessages);
        Assert.That(response.ErrorMessages, Has.Count.EqualTo(1));
        Assert.That(response.ErrorMessages[0], Is.EqualTo("Title length has to be between 3 and 75 characters."));
        return Task.CompletedTask;
    }
}