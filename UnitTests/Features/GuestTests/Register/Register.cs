using ViaEventAssociantion.Core.domain;
using ViaEventAssociation.Core.Domain.Aggregates.Users;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.Register;

public class Register
{
    [SetUp]
    public void Setup()
    {
       
    }

    [Test]
    public void Register_S1()
    {
        //Arrange
        int id = 0001;
        string username = "bob1";
        string email = "315225@via.dk";
        string password = "veryStrongPasswor22E@d";

        //Act
        ResultBase result = Anonymous.RegisterUserAccount(id,username,password,email);
        Guest registeredUser = ((Result<Guest>)result).Values;

        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(registeredUser.Username.Value, Is.EqualTo(username));
        Assert.That(registeredUser.Password.Value, Is.EqualTo(password));
    }
    
    [Test]
    public void Register_F1()
    {
        //Arrange
        int id = 0001;
        string username = "bob1";
        string email = "315225";
        string password = "veryStrongPasswor22E@d";

        //Act
        ResultBase result = Anonymous.RegisterUserAccount(id,username,password,email);

        //Assert
        Assert.That(result.ErrorMessages[0], Is.EqualTo("Email must be in the correct format (e.g., example@via.dk)."));
        Assert.IsTrue(!result.IsSuccess);
    }
    
    [Test]
    public void Register_F2()
    {
        //Arrange
        int id = 0001;
        string username = "bob1";
        string email = "31@.com";
        string password = "veryStrongPasswor22E@d";

        //Act
        ResultBase result = Anonymous.RegisterUserAccount(id,username,password,email);

        //Assert
        Assert.That(result.ErrorMessages[0], Is.EqualTo("Email must be in the correct format (e.g., example@via.dk)."));
        Assert.IsTrue(!result.IsSuccess);
    }
    
    [Test]
    public void Register_F3()
    {
        // To add firstname across system
      
        //Arrange
        //string firstName = ".";
        //string lastName = "mario";
        //string email = "123123@via.dk";
        //string password = "veryStrongPasswor22E@d";
        //ResultBase result = Anonymous.Create();
        //Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        //ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        //Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        //Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("First name must be between 2 and 25 characters long and contain only letters."));
    }
    
    [Test]
    public void Register_F4()
    {
        // To add lastname across system
      
      
        //Arrange
        //string firstName = "bob";
        //string lastName = ".";
        //string email = "123123@via.dk";
        //string password = "veryStrongPasswor22E@d";
        //ResultBase result = Anonymous.Create();
        //Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        //ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        //Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        //Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("Last name must be between 2 and 25 characters long and contain only letters."));
    }
    
    [Test]
    public void Register_F5()
    {
        // Database needed for this testcase
    }
    
    [Test]
    public void Register_F6()
    {
        // To add lastname across system
      
        //Arrange
        //string firstName = "bob9999";
        //string lastName = "mario";
        //string email = "123123@via.dk";
        //string password = "veryStrongPasswor22E@d";
        //ResultBase result = Anonymous.Create();
        //Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        //ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        //Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        //Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("First name must be between 2 and 25 characters long and contain only letters."));
    }
    
    [Test]
    public void Register_F7()
    {
        // To add lastname across system

        //Arrange
        //string firstName = "$bob$";
        //string lastName = "mario";
        //string email = "123123@via.dk";
        //string password = "veryStrongPasswor22E@d";
        //ResultBase result = Anonymous.Create();
        //Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        //ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        //Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        //Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("First name must be between 2 and 25 characters long and contain only letters."));
    }
}