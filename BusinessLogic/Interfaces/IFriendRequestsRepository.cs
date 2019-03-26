using Domain.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IFriendRequestsRepository
    {
        IEnumerable<FriendRequest> GetFriendRequests();
        bool RequestIsSent(int userFromId, int userToId);
        void AddFriendRequest(FriendRequest friendRequest);
        void DeleteFriendRequest(FriendRequest friendRequest);
    }
}
