using Domain.Entities;

namespace Web.Models
{
    public class UserViewModel
    {
        //Пользователь
        public User User { get; set; }

        //Вызываемый пользователь - это я сам
        public bool UserIsMe { get; set; }

        //Этот пльзователь - мой друг
        public bool UserIsMyFriend { get; set; }

        //Я уже отправил этому пользователю заявку в друзья
        public bool FriendRequestIsSent { get; set; }
    }
}