using Domain.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IFriendsRepository
    {
        IEnumerable<Friend> GetFriends();
        bool UsersAreFriends(int userId, int user2Id);
        void AddFriend(Friend friend);
        void DeleteFriend(Friend friend);
    }
}
