using NUnit.Framework;
using System.Collections.Generic;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Common.OperationResult
{
    [TestFixture]
    public class OperationResultTest
    {
        [Test]
        public void TestResultBaseConstructor()
        {
            // Arrange
            List<string> errorMessages = new List<string> { "Error 1", "Error 2" };

            // Act
            ResultBase resultBase = new ResultBase(errorMessages);

            // Assert
            Assert.IsNotNull(resultBase);
            Assert.AreEqual(errorMessages, resultBase.ErrorMessages);
            Assert.IsFalse(resultBase.IsSuccess);
        }

        [Test]
        public void TestResultBaseDefaultConstructor()
        {
            // Act
            ResultBase resultBase = new ResultBase();

            // Assert
            Assert.IsNotNull(resultBase);
            Assert.IsNotNull(resultBase.ErrorMessages);
            Assert.AreEqual(0, resultBase.ErrorMessages.Count);
            Assert.IsTrue(resultBase.IsSuccess);
        }
    }
}