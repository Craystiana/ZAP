using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZAP.BusinessLogic.DTO.User;
using ZAP.BusinessLogic.Services;
using ZAP.Common.Enums;
using ZAP.DataAccess.Interfaces;
using ZAP.UnitTests.TestRepositories;

namespace ZAP.UnitTests.ServiceTests
{
    [TestClass]
    public class UserServiceTest
    {
        private IUnitOfWork _unitOfWork;
        private UserService _userService;

        [TestInitialize]
        public void InitializeData()
        {
            _unitOfWork = new TestUnitOfWork();
            _userService = new UserService(_unitOfWork);
        }

        [TestMethod]
        public void UserLogin_ShouldWorkForNewUser_WithCorrectPassword()
        {
            var userRegisterModel = new RegisterModel
            {
                FirstName = "Register test",
                LastName = "Last name",
                Address = "Random Address",
                Email = "test@zap.com",
                PhoneNumber ="14121412412",
                Password = "HashTest*0"
            };

            _userService.Register(userRegisterModel);

            var result = _userService.VerifyIdentity(userRegisterModel.Email, userRegisterModel.Password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserLogin_ShouldFailForNewUser_WithWrongPassword()
        {
            var userRegisterModel = new RegisterModel
            {
                FirstName = "Register test",
                LastName = "Last name",
                Address = "Random Address",
                Email = "test1@zap.com",
                PhoneNumber = "14121412412",
                Password = "HashTest*0"
            };

            _userService.Register(userRegisterModel);

            var result = _userService.VerifyIdentity(userRegisterModel.Email, "Wrong Password");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UserRegister_ShouldFail_WhenUsingExistingEmail()
        {
            var userRegisterModel1 = new RegisterModel
            {
                FirstName = "Register test",
                LastName = "Last name",
                Address = "Random Address",
                Email = "test2@zap.com",
                PhoneNumber = "14121412412",
                Password = "HashTest*0"
            };

            _userService.Register(userRegisterModel1);

            var userRegisterModel2 = new RegisterModel
            {
                FirstName = "Register test 2",
                LastName = "Last name 2",
                Address = "Random Address 2",
                Email = "test2@zap.com",
                PhoneNumber = "1412312",
                Password = "HashTest*0"
            };

            var result = _userService.Register(userRegisterModel2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UserLogin_ShouldFail_ForNonexistentUser()
        {
            var result = _userService.VerifyIdentity("unexistent mail", "password");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GeneratedPasswordHash_ShouldBeTheSame_ForTheSamePasswordAndSaltCombination()
        {
            var password = "password 412611 (&^%$#@#$%^& password";
            var salt = _userService.GenerateSalt();

            var hash1 = _userService.CreatePasswordHash(password, salt);
            var hash2 = _userService.CreatePasswordHash(password, salt);

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void RegisterUser_CustomerShouldHaveCustomerRole()
        {
            var adminRegister = new RegisterModel
            {
                Email = "customer_test@zap.com",
                FirstName = "Customer test",
                LastName = "Customer test",
                Password = "admin"
            };

            _userService.Register(adminRegister);

            var user = _userService.GetUser(adminRegister.Email);

            Assert.IsTrue(user.UserRoleId == (int)UserRoleType.Customer);
        }

        [TestMethod]
        public void RegisterUser_AdminShouldHaveAdminRole()
        {
            var adminRegister = new RegisterModel
            {
                Email = "Admin_test@zap.com",
                FirstName = "Admin test",
                LastName = "Admin test",
                Password = "admin"
            };

            _userService.RegisterAdmin(adminRegister);

            var user = _userService.GetUser(adminRegister.Email);

            Assert.IsTrue(user.UserRoleId == (int)UserRoleType.Admin);
        }

        [TestMethod]
        public void UserProfile_AddressUpdates()
        {
            var newAddress = "New Address";
            var user = _userService.GetUser("admin@zap.com");

            var profile = _userService.GetProfile(user.UserId);

            profile.Address = newAddress;

            _userService.UpdateProfile(profile, user.UserId);

            profile = _userService.GetProfile(user.UserId);

            Assert.AreEqual(profile.Address, newAddress);
        }

        [TestMethod]
        public void UserProfile_PhoneNumberUpdates()
        {
            var newPhoneNumber = "0512151124";
            var user = _userService.GetUser("admin@zap.com");

            var profile = _userService.GetProfile(user.UserId);

            profile.PhoneNumber = newPhoneNumber;

            _userService.UpdateProfile(profile, user.UserId);

            profile = _userService.GetProfile(user.UserId);

            Assert.AreEqual(profile.PhoneNumber, newPhoneNumber);
        }
    }
}
