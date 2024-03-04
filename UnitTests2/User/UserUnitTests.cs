using NUnit.Framework;
using ViaEventAssociantion.Core.domain.UserProperties;

namespace UnitTests.User;

[TestFixture]
public class UserUnitTests
{

    [SetUp]
    public void Setup()
    {
        var userId = new UserId();
        var username = new Username();
        var password = new Password();
    }


    [Test]
    // Will add conditions in the username, then we can add tests that the user creation fails with invalid names
    public async Task RegisterUser_ValidData_Success()
    {
        //Arrange
        var user = new ViaEventAssociantion.Core.domain.User();
        
        //Act
        user.Register("rob123", "password123");
        
        //Assert
        Assert.AreEqual("rob123", user.Username);
        Assert.AreEqual("password123", user.Password);
    }
    
    
}