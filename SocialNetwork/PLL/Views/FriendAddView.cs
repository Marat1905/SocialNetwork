using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class FriendAddView
    {
        private readonly FriendService _friendService;
        private readonly UserService _userService;

        public FriendAddView()
        {
            _friendService = new FriendService();
            _userService = new UserService();
        }
        public FriendAddView(FriendService friendService, UserService userService)
        {
            _friendService = friendService;
            _userService = userService;
        }

        public void Show(User user)
        {
            var friendData = new FriendData();

            Console.Write("Введите почтовый адрес для добавления в друзья: ");
            friendData.FriendEmail = Console.ReadLine();

            friendData.UserId = user.Id;

            try
            {
                _friendService.AddFriend(friendData);
                SuccessMessage.Show("Друг успешно добавлен!");
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }

            catch (ArgumentNullException)
            {
                AlertMessage.Show("Введите корректное значение!");
            }

            catch (AlreadyFriendsException)
            {
                AlertMessage.Show("Пользователь был ранее добавлен в друзья!");
            }

            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка при отправке сообщения!");
            }
        }
    }
}
