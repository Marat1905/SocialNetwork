using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.BLL.Services
{
    public class FriendService
    {
        readonly IUserRepository _userRepository;
        readonly IFriendRepository _friendRepository;
        public FriendService() 
        {
            _userRepository = new UserRepository();
            _friendRepository = new FriendRepository();
        }

        public FriendService(IUserRepository userRepository, IFriendRepository friendRepository)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
        }


        public void AddFriend(FriendData friendData)
        {
            if (string.IsNullOrWhiteSpace(friendData.FriendEmail))
                throw new ArgumentNullException();        

            var findUserEntity = _userRepository.FindByEmail(friendData.FriendEmail);
            if (findUserEntity is null) 
                throw new UserNotFoundException();

            if (GetAllFriends(friendData.UserId).ToList().Any(n => n.FriendId == findUserEntity.id))
                throw new AlreadyFriendsException();


            var friendEntity = new FriendEntity
            {
                user_id = friendData.UserId,
                friend_id = findUserEntity.id
            };

            if (_friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }


        public IEnumerable<Friend> GetAllFriends(int Id)
        {
            var messages = new List<Friend>();

            var f = _friendRepository.FindAllByUserId(Id).ToList();
            _friendRepository.FindAllByUserId(Id).ToList().ForEach(m =>
            {
                var friendEntity = _userRepository.FindById(m.friend_id);

                messages.Add(new Friend(friendEntity.id,friendEntity.firstname,friendEntity.email));
            });

            _friendRepository.FindAllByUserId(Id);
            return messages;
        }
    }
}
