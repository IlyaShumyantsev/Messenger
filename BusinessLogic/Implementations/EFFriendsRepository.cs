using BusinessLogic.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementations
{
    public class EFFriendsRepository : IFriendsRepository
    {
        private EFDbContext context;
        public EFFriendsRepository(EFDbContext context)
        {
            this.context = context;
        }

        public void AddFriend(Friend friend)
        {
            context.Friends.Add(friend);
            context.SaveChanges();
        }

        public void DeleteFriend(Friend friend)
        {
            context.Friends.Remove(friend);
            context.SaveChanges();
        }

        public IEnumerable<Friend> GetFriends()
        {
            return context.Friends;
        }

        public bool UsersAreFriends(int userId, int user2Id)
        {
            return context.Friends.Count(x => x.UserId == userId && x.FriendId == user2Id) != 0;
        }
    }
}
