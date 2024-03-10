using ViaEventAssociantion.Core.domain.Enums;
using ViaEventAssociantion.Core.domain.EventProperties;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateTimeRange;

[TestFixture]
public class UpdateTimeRange
{
    [Test]
    [TestCase(2023, 8, 25, 19, 0, 0, 2023, 8, 25, 23, 59, 0)]
    [TestCase(2023, 8, 25, 12, 0, 0, 2023, 8, 25, 16, 30, 0)]
    [TestCase(2023, 8, 25, 8, 0, 0, 2023, 8, 25, 12, 15, 0)]
    [TestCase(2023, 8, 25, 10, 0, 0, 2023, 8, 25, 20, 0, 0)]
    [TestCase(2023, 8, 25, 13, 0, 0, 2023, 8, 25, 23, 0, 0)]
    public Task UpdateTimeRange_S1(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        // Act
        createdEvent.UpdateTimeRange(startTime, endTime);

        // Assert
        Assert.That(createdEvent.StartTime, Is.EqualTo(startTime));
        Assert.That(createdEvent.EndTime, Is.EqualTo(endTime));
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 25, 19, 0, 0, 2023, 8, 26, 1, 0, 0)]
    [TestCase(2023, 8, 25, 12, 0, 0, 2023, 8, 25, 16, 30, 0)]
    [TestCase(2023, 8, 25, 8, 0, 0, 2023, 8, 25, 12, 15, 0)]
    public Task UpdateTimeRange_S2(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        // Act
        createdEvent.UpdateTimeRange(startTime, endTime);

        // Assert
        Assert.That(createdEvent.StartTime, Is.EqualTo(startTime));
        Assert.That(createdEvent.EndTime, Is.EqualTo(endTime));
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 25, 19, 0, 0, 2023, 8, 26, 1, 0, 0)]
    [TestCase(2023, 8, 25, 12, 0, 0, 2023, 8, 25, 16, 30, 0)]
    [TestCase(2023, 8, 25, 8, 0, 0, 2023, 8, 25, 12, 15, 0)]
    public Task UpdateTimeRange_S3(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);
        createdEvent.Status = EventStatus.Ready;

        // Act
        createdEvent.UpdateTimeRange(startTime, endTime);

        // Assert
        Assert.That(createdEvent.StartTime, Is.EqualTo(startTime));
        Assert.That(createdEvent.EndTime, Is.EqualTo(endTime));
        Assert.That(createdEvent.Status, Is.EqualTo(EventStatus.Draft));
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2025, 8, 25, 19, 0, 0, 2025, 8, 26, 1, 0, 0)]
    [TestCase(2025, 8, 25, 12, 0, 0, 2025, 8, 25, 16, 30, 0)]
    [TestCase(2025, 8, 25, 8, 0, 0, 2025, 8, 25, 12, 15, 0)]
    public Task UpdateTimeRange_S4(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        // Act
        createdEvent.UpdateTimeRange(startTime, endTime);

        // Assert
        Assert.That(createdEvent.StartTime, Is.EqualTo(startTime));
        Assert.That(createdEvent.EndTime, Is.EqualTo(endTime));
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 25, 10, 0, 0, 2023, 8, 25, 20, 0, 0)]
    [TestCase(2023, 8, 25, 12, 0, 0, 2023, 8, 25, 16, 30, 0)]
    [TestCase(2023, 8, 25, 8, 0, 0, 2023, 8, 25, 12, 15, 0)]
    public Task UpdateTimeRange_S5(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        // Act
        createdEvent.UpdateTimeRange(startTime, endTime);

        // Assert
        Assert.That(createdEvent.StartTime, Is.EqualTo(startTime));
        Assert.That(createdEvent.EndTime, Is.EqualTo(endTime));
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 26, 19, 0, 0, 2023, 8, 25, 1, 0, 0)]
    [TestCase(2023, 8, 26, 19, 0, 0, 2023, 8, 25, 23, 59, 0)]
    [TestCase(2023, 8, 27, 12, 0, 0, 2023, 8, 25, 16, 30, 0)]
    [TestCase(2023, 8, 1, 8, 0, 0, 2023, 7, 31, 12, 15, 0)]
    public Task UpdateTimeRange_F1(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.That(response.ErrorMessages, Contains.Item("End time cannot be before start time."));
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 26, 19, 0, 0, 2023, 8, 26, 14, 0, 0)]
    [TestCase(2023, 8, 26, 16, 0, 0, 2023, 8, 26, 0, 0, 0)]
    [TestCase(2023, 8, 26, 19, 0, 0, 2023, 8, 26, 18, 59, 0)]
    [TestCase(2023, 8, 26, 12, 0, 0, 2023, 8, 26, 10, 10, 0)]
    [TestCase(2023, 8, 26, 8, 0, 0, 2023, 8, 26, 0, 30, 0)]
    public Task UpdateTimeRange_F2(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.That(response.ErrorMessages, Contains.Item("End time cannot be before start time."));
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 26, 14, 0, 0, 2023, 8, 26, 14, 50, 0)]
    [TestCase(2023, 8, 26, 18, 0, 0, 2023, 8, 26, 18, 59, 0)]
    [TestCase(2023, 8, 26, 12, 0, 0, 2023, 8, 26, 12, 30, 0)]
    [TestCase(2023, 8, 26, 8, 0, 0, 2023, 8, 26, 8, 0, 0)]
    public Task UpdateTimeRange_F3(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.That(
            response.ErrorMessages,
            Contains.Item("The event has to be at least 1 hour long.")
        );
        return Task.CompletedTask;
    }

    [TestCase(2023, 8, 25, 23, 30, 0, 2023, 8, 26, 0, 15, 0)]
    [TestCase(2023, 8, 30, 23, 1, 0, 2023, 8, 31, 0, 0, 0)]
    [TestCase(2023, 8, 30, 23, 59, 0, 2023, 8, 31, 0, 1, 0)]
    public Task UpdateTimeRange_F4(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.That(
            response.ErrorMessages,
            Contains.Item("The event has to be at least 1 hour long.")
        );
        return Task.CompletedTask;
    }

    [TestCase(2023, 8, 25, 7, 50, 0, 2023, 8, 25, 14, 0, 0)]
    [TestCase(2023, 8, 25, 7, 59, 0, 2023, 8, 25, 15, 0, 0)]
    [TestCase(2023, 8, 25, 1, 1, 0, 2023, 8, 25, 8, 30, 0)]
    [TestCase(2023, 8, 25, 5, 59, 0, 2023, 8, 25, 7, 59, 0)]
    [TestCase(2023, 8, 25, 0, 59, 0, 2023, 8, 25, 7, 59, 0)]
    public Task UpdateTimeRange_F5(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Rooms are only usable between 8 AM and 1 AM on the next day.")
            )
        );
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 24, 23, 50, 0, 2023, 8, 25, 1, 1, 1)]
    [TestCase(2023, 8, 24, 22, 0, 0, 2023, 8, 25, 7, 59, 0)]
    [TestCase(2023, 8, 30, 23, 0, 0, 2023, 8, 31, 2, 30, 0)]
    [TestCase(2023, 8, 24, 23, 50, 0, 2023, 8, 25, 1, 1, 0)]
    public Task UpdateTimeRange_F6(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Rooms are only usable between 8 AM and 1 AM on the next day.")
            )
        );
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 25, 19, 0, 0, 2023, 8, 26, 1, 0, 0)]
    public Task UpdateTimeRange_F7(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);
        createdEvent.Status = EventStatus.Active;

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Cannot update the time range of an active event.")
            )
        );
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 25, 19, 0, 0, 2023, 8, 26, 1, 0, 0)]
    public Task UpdateTimeRange_F8(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);
        createdEvent.Status = EventStatus.Cancelled;

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Cannot update the time range of a cancelled event.")
            )
        );
        return Task.CompletedTask;
    }

    [Test]
    [TestCase(2023, 8, 30, 8, 0, 0, 2023, 8, 30, 18, 1, 0)]
    [TestCase(2023, 8, 30, 14, 59, 0, 2023, 8, 31, 1, 0, 0)]
    [TestCase(2023, 8, 30, 14, 0, 0, 2023, 8, 31, 0, 1, 0)]
    [TestCase(2023, 8, 30, 14, 0, 0, 2023, 8, 31, 18, 30, 0)]
    public Task UpdateTimeRange_F9(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("The event has to be at most 10 hours long.")
            )
        );
        return Task.CompletedTask;
    }

    // Commented out as all test scenarios have a past date

    // [Test]
    // [TestCase(2023, 8, 30, 8, 0, 0, 2023, 8, 30, 18, 1, 0)]
    // public Task UpdateTimeRange_F10(
    //     int startYear,
    //     int startMonth,
    //     int startDay,
    //     int startHour,
    //     int startMinute,
    //     int startSecond,
    //     int endYear,
    //     int endMonth,
    //     int endDay,
    //     int endHour,
    //     int endMinute,
    //     int endSecond
    // )
    // {
    //     // Arrange
    //     var eventId = new EventId(1);
    //     var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
    //     ViaEventAssociantion.Core.domain.Event createdEvent = (
    //         (Result<ViaEventAssociantion.Core.domain.Event>)result
    //     ).Values;
    //
    //     var startTime = new DateTime(
    //         startYear,
    //         startMonth,
    //         startDay,
    //         startHour,
    //         startMinute,
    //         startSecond
    //     );
    //     var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);
    //
    //     //Act
    //     ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);
    //
    //     //Assert
    //     Assert.IsFalse(response.IsSuccess);
    //     Assert.IsNotNull(response.ErrorMessages);
    //     Assert.IsTrue(
    //         response.ErrorMessages.Any(message =>
    //             message.Contains("Start time cannot be in the past.")
    //         )
    //     );
    //     return Task.CompletedTask;
    // }

    [Test]
    [TestCase(2023, 8, 31, 0, 30, 0, 2023, 8, 31, 8, 30, 0)]
    [TestCase(2023, 8, 30, 23, 59, 0, 2023, 8, 31, 8, 1, 0)]
    [TestCase(2023, 8, 31, 1, 0, 0, 2023, 8, 31, 8, 0, 0)]
    public Task UpdateTimeRange_F11(
        int startYear,
        int startMonth,
        int startDay,
        int startHour,
        int startMinute,
        int startSecond,
        int endYear,
        int endMonth,
        int endDay,
        int endHour,
        int endMinute,
        int endSecond
    )
    {
        // Arrange
        var eventId = new EventId(1);
        var result = ViaEventAssociantion.Core.domain.Event.Create(eventId);
        ViaEventAssociantion.Core.domain.Event createdEvent = (
            (Result<ViaEventAssociantion.Core.domain.Event>)result
        ).Values;

        var startTime = new DateTime(
            startYear,
            startMonth,
            startDay,
            startHour,
            startMinute,
            startSecond
        );
        var endTime = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

        //Act
        ResultBase response = createdEvent.UpdateTimeRange(startTime, endTime);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNotNull(response.ErrorMessages);
        Assert.IsTrue(
            response.ErrorMessages.Any(message =>
                message.Contains("Rooms are only usable between 8 AM and 1 AM on the next day.")
            )
        );
        return Task.CompletedTask;
    }
}
