using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.Ready;

public class Ready
{
    private ViaEventAssociantion.Core.domain.Event createdEvent;
    private ViaEventAssociantion.Core.domain.Event validCreatedEvent;

    [SetUp]
    public void Setup()
    {
        EventId eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        createdEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result).Values;

        EventId eventId2 = new EventId(1);
        var result2 = ViaEventAssociantion.Core.domain.Event.Create(eventId2);
        validCreatedEvent = ((Result<ViaEventAssociantion.Core.domain.Event>)result2).Values;
        validCreatedEvent.SetEventStatus(EventStatus.Draft);
        validCreatedEvent.MakeEventPrivate();
        validCreatedEvent.UpdateTitle("Great event");
        validCreatedEvent.UpdateDescription("Amazingly great event!");
        DateTime currentTime = DateTime.Now;
        DateTime startTime = currentTime.AddHours(-1);
        DateTime endTime = currentTime.AddHours(2);
        validCreatedEvent.StartTime = startTime;
        validCreatedEvent.EndTime = endTime;
        validCreatedEvent.SetMaxNrOfGuests(19);
    }

    [Test]
    public void UpdateDescription_S1()
    {
        //Act
        ResultBase resultBase = validCreatedEvent.MakeEventReady();

        //Assert
        Assert.IsTrue(resultBase.IsSuccess);
        Assert.That(validCreatedEvent.Status, Is.EqualTo(EventStatus.Ready));
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(0));
    }

    [Test]
    public void UpdateDescription_F1()
    {
        //Arrange
        validCreatedEvent.SetEventStatus(EventStatus.Draft);

        //Act
        ResultBase resultBase = createdEvent.MakeEventReady();

        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(
            string.Join(", ", resultBase.ErrorMessages),
            Is.EqualTo(
                "Title must be set., Description must be set., Start date time must be set., End date time must be set."
            )
        );
    }

    [Test]
    public void UpdateDescription_F2()
    {
        //Arrange
        createdEvent.SetEventStatus(EventStatus.Cancelled);

        //Act
        ResultBase resultBase = createdEvent.MakeEventReady();

        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(
            resultBase.ErrorMessages[0],
            Is.EqualTo("A cancelled event cannot be readied.")
        );
    }

    [Test]
    public void UpdateDescription_F3()
    {
        //Arrange
        DateTime currentTime = DateTime.Now;
        DateTime startTime = currentTime.AddHours(1);
        DateTime endTime = currentTime.AddHours(3);
        validCreatedEvent.UpdateTimeRange(startTime, endTime);

        //Act
        ResultBase resultBase = validCreatedEvent.MakeEventReady();

        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages.Count, Is.EqualTo(1));
        Assert.That(
            resultBase.ErrorMessages[0],
            Is.EqualTo("An event in the past cannot be made ready.")
        );
    }

    [Test]
    public void UpdateDescription_F4()
    {
        //Arrange
        validCreatedEvent.SetEventStatus(EventStatus.Cancelled);
        validCreatedEvent.UpdateTitle("");

        //Act
        ResultBase resultBase = createdEvent.MakeEventReady();

        //Assert
        Assert.IsTrue(!resultBase.IsSuccess);
        Assert.That(resultBase.ErrorMessages[0], Is.EqualTo("Title must be set."));
    }
}
