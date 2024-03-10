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
        string firstName = "bob";
        string lastName = "mario";
        string email = "123123@via.dk";
        string password = "veryStrongPasswor22E@d";
        ResultBase result = Anonymous.Create();
        Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        Guest registeredUser = ((Result<Guest>)resultRegisteredUser).Values;

        //Assert
        Assert.IsTrue(resultRegisteredUser.IsSuccess);
        Assert.That(registeredUser.UserFirstName.toString(), Is.EqualTo("Bob"));
        Assert.That(registeredUser.UserLastName.toString(), Is.EqualTo("Mario"));
    }
    
    [Test]
    public void Register_F1()
    {
        //Arrange
        string firstName = "bob";
        string lastName = "mario";
        string email = "123123@gmail.dk";
        string password = "veryStrongPasswor22E@d";
        ResultBase result = Anonymous.Create();
        Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("Email must be in the correct format (e.g., example@via.dk)."));
    }
    
    [Test]
    public void Register_F2()
    {
        //Arrange
        string firstName = "bob";
        string lastName = "mario";
        string email = "123123@gmail.dk";
        string password = "veryStrongPasswor22E@d";
        ResultBase result = Anonymous.Create();
        Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("Email must be in the correct format (e.g., example@via.dk)."));
    }
    
    [Test]
    public void Register_F3()
    {
        //Arrange
        string firstName = ".";
        string lastName = "mario";
        string email = "123123@via.dk";
        string password = "veryStrongPasswor22E@d";
        ResultBase result = Anonymous.Create();
        Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("First name must be between 2 and 25 characters long and contain only letters."));
    }
    
    [Test]
    public void Register_F4()
    {
        //Arrange
        string firstName = "bob";
        string lastName = ".";
        string email = "123123@via.dk";
        string password = "veryStrongPasswor22E@d";
        ResultBase result = Anonymous.Create();
        Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("Last name must be between 2 and 25 characters long and contain only letters."));
    }
    
    [Test]
    public void Register_F5()
    {
       // Database needed 
    }
    
    [Test]
    public void Register_F6()
    {
        //Arrange
        string firstName = "bob9999";
        string lastName = "mario";
        string email = "123123@via.dk";
        string password = "veryStrongPasswor22E@d";
        ResultBase result = Anonymous.Create();
        Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("First name must be between 2 and 25 characters long and contain only letters."));
    }
    
    [Test]
    public void Register_F7()
    {
        //Arrange
        string firstName = "$bob$";
        string lastName = "mario";
        string email = "123123@via.dk";
        string password = "veryStrongPasswor22E@d";
        ResultBase result = Anonymous.Create();
        Anonymous anonymousUser = ((Result<Anonymous>)result).Values;

        //Act
        ResultBase resultRegisteredUser = anonymousUser.RegisterUserAccount(email, firstName, lastName, password);
        
        //Assert
        Assert.IsTrue(!resultRegisteredUser.IsSuccess);
        Assert.That(resultRegisteredUser.ErrorMessages[0], Is.EqualTo("First name must be between 2 and 25 characters long and contain only letters."));
    }
}