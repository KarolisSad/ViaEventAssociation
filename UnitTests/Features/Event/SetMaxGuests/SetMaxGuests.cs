using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.SetMaxGuests;

public class SetMaxGuests
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
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(25)]
    [TestCase(50)]
    public void UpdateDescription_S1(int maxNumberOfGuests)
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Ready);

        //Act
        ResultBase resultBase = createdEvent.SetMaxNrOfGuests(maxNumberOfGuests);

        //Assert
        Assert.IsTrue(resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(0));
        Assert.That(createdEvent.MaximumNumberOfGuests, Is.EqualTo(maxNumberOfGuests));
    }

    [Test]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(25)]
    [TestCase(50)]
    public void UpdateDescription_S2(int maxNumberOfGuests)
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Ready);

        //Act
        ResultBase resultBase = createdEvent.SetMaxNrOfGuests(maxNumberOfGuests);

        //Assert
        Assert.IsTrue(resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(0));
        Assert.That(createdEvent.MaximumNumberOfGuests, Is.EqualTo(maxNumberOfGuests));
    }

    [Test]
    [TestCase(5)]
    [TestCase(50)]
    public void UpdateDescription_S3(int maxNumberOfGuests)
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Active);

        //Act
        ResultBase resultBase = createdEvent.SetMaxNrOfGuests(maxNumberOfGuests);

        //Assert
        Assert.IsTrue(resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(0));
        Assert.That(createdEvent.MaximumNumberOfGuests, Is.EqualTo(maxNumberOfGuests));
    }

    [Test]
    public void UpdateDescription_F1()
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Active);
        createdEvent.SetMaxNrOfGuests(10);

        //Act
        ResultBase resultBase = createdEvent.SetMaxNrOfGuests(5);

        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(
            resultBase.ErrorMessages[0],
            Is.EqualTo("Maximum number of guests for an active event cannot be reduced.")
        );
    }

    [Test]
    public void UpdateDescription_F2()
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Cancelled);

        //Act
        ResultBase resultBase = createdEvent.SetMaxNrOfGuests(6);

        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(
            resultBase.ErrorMessages[0],
            Is.EqualTo("A cancelled event cannot be modified.")
        );
    }

    [Test]
    public void UpdateDescription_F3()
    {
        // Need: UC16-20 (Locations)
    }

    [Test]
    public void UpdateDescription_F4()
    {
        //Arrange

        //Act
        ResultBase resultBase = createdEvent.SetMaxNrOfGuests(2);

        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);

        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(
            resultBase.ErrorMessages[0],
            Is.EqualTo("The maximum number of guests cannot be less than 5.")
        );
    }

    [Test]
    public void UpdateDescription_F5()
    {
        //Act
        ResultBase resultBase = createdEvent.SetMaxNrOfGuests(51);

        //Assert
        // Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(
            resultBase.ErrorMessages[0],
            Is.EqualTo("The maximum number of guests cannot be more than 50.")
        );
    }
}
