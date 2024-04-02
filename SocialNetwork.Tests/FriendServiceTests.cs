using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using System.Diagnostics;

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class FriendServiceTests
    {
        UserService _userService;
        FriendService _friendService;
        IUserRepository _userRepository;
        IFriendRepository _friendRepository;

        [SetUp]
        public void SetUp()
        {
            _friendService = new FriendService();
            _userService= new UserService();
            _userRepository= new UserRepository();
            _friendRepository= new FriendRepository();
            UserRegistrationData Antonov = new UserRegistrationData()
            {
                Email = "gmail@gmail.com",
                FirstName = "Антон",
                LastName = "Антонов",
                Password = "12121212"
            };
            UserRegistrationData Petrov = new UserRegistrationData()
            {
                Email = "gmail1@gmail.com",
                FirstName = "Петр",
                LastName = "Петров",
                Password = "12121212"
            };
            _userService.Register(Antonov);
            _userService.Register(Petrov);
        }

        [Test]
        public void AddFriend_MustAlwaysReturnsExpectedValue()
        {
            FriendData friendData = new FriendData()
            {
                UserId = 1,
                FriendEmail = "gmail1@gmail.com"
            };

            _friendService.AddFriend(friendData);
            _friendService.GetAllFriends(friendData.UserId).Count();

             Assert.That(_friendService.GetAllFriends(friendData.UserId).Count(), Is.EqualTo(1));
        }

        [Test]
        public void AddFriend_MustThrowArgumentNullException()
        {
            FriendData friendData= new FriendData();      
            Assert.Throws<ArgumentNullException>(() => _friendService.AddFriend(friendData));        
        }

        [Test]
        public void AddFriend_MustThrowAlreadyFriendsException()
        {
            FriendData friendData = new FriendData()
            {
                UserId = 1,
                FriendEmail = "gmail1@gmail.com"
            };

            _friendService.AddFriend(friendData);
            Assert.Throws<AlreadyFriendsException>(() => _friendService.AddFriend(friendData));
        }

        [Test]
        public void AddFriend_MustThrowUserNotFoundException()
        {
            FriendData friendData = new FriendData()
            {
                UserId = 1,
                FriendEmail = "gmail2@gmail.com"
            };

            Assert.Throws<UserNotFoundException>(() => _friendService.AddFriend(friendData));
        }



        [TearDown]
        public void Finish()
        {
           _userRepository.DeleteAll();
            _friendRepository.DeleteAll();

        }
    }
}
