using WebApi.Services;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WebApi.Models;
using WebApi.Entities;

namespace WebApiTest
{
    [TestClass]
    public class UserServiceTest
    {
        IUserService _userService;
        
        AuthenticateRequest authRequest = new()
        {
            Password = "test",
            Username = "test"
        };

        User validUser = new()
        {
            Id = 1,
            Password = "test",
            Username = "test"
        };


        User invalidUser = new()
        {
            Id = 0,
            Password = "test",
            Username = "test"
        };

        const string Token = "token";
        Mock<IUserService> mock;


        [TestInitialize]
        public void Setup()
        {

            mock = new Mock<IUserService>();
            _userService = mock.Object;
        }

        [TestMethod]
        public void ShouldAuthenticateUser()
        {
            var authenticatedResponse = new AuthenticateResponse(validUser, Token, true);
            mock.Setup(service => service.Authenticate(authRequest))
                .Returns(authenticatedResponse);
            Assert.AreEqual(authenticatedResponse, _userService.Authenticate(authRequest));
        }


        [TestMethod]
        public void ShouldNotAuthenticateUser()
        {
            var notAuthorizedRespose = new AuthenticateResponse(invalidUser);
            mock.Setup(service => service.Authenticate(authRequest))
                .Returns(notAuthorizedRespose);
            Assert.AreEqual(notAuthorizedRespose, _userService.Authenticate(authRequest));
        }

        [TestMethod]
        public void ShouldReturnsUserById()
        {
            const int userId = 1;
            var notAuthorizedRespose = new AuthenticateResponse(invalidUser);
            mock.Setup(service => service.GetById(userId))
                .Returns(validUser);
            Assert.AreEqual(validUser, _userService.GetById(userId));
        }
    }
}
