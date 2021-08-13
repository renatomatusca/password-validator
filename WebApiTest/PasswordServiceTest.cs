using WebApi.Services;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace WebApiTest
{
    [TestClass]
    public class PasswordServiceTest
    {
        const string ValidPassword = "i2owpgje8I2946@";
        const string InvalidPassword = "i1231111owpgje8I2";
        const int MinLenght = 15;
        char[] RequiredSpecialChars = new[] { '@', '#', '_', '-', '!' };

        IPasswordService _passwordService;

        [TestInitialize]
        public void Setup()
        {
            var moc = new Mock<IPasswordService>();
            moc.Setup(service => service.ValidatePassword(ValidPassword))
                .Returns(true);
            
            moc.Setup(service => service.ValidatePassword(InvalidPassword))
                .Returns(false);

            moc.Setup(service => service.GetValidPassword())
                .Returns(ValidPassword);


            _passwordService = moc.Object;
        }

        [TestMethod]
        public void ShouldValidatePassword()
        {
            Assert.IsTrue(_passwordService.ValidatePassword(ValidPassword));
        }

        [TestMethod]
        public void ShouldNotValidatePassword()
        {
            Assert.IsFalse(_passwordService.ValidatePassword(InvalidPassword));
        }

        [TestMethod]
        public void ShouldGenerateValidPassword()
        {
            var password = _passwordService.GetValidPassword();
            Assert.IsNotNull(password);
            Assert.IsTrue(password.Length >= MinLenght);
            Assert.IsTrue(password.Any(x => char.IsUpper(x)));
            Assert.IsTrue(password.Any(x => char.IsLower(x)));
            Assert.IsTrue(password.Any(x => RequiredSpecialChars.Contains(x)));
            Assert.IsTrue(_passwordService.ValidatePassword(password));
        }
    }
}
