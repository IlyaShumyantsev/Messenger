using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    //Класс, через который централизованно происходит обмен данными в приложении
    public class DataManager
    {
        private IUsersRepository usersRepository;
        private IFriendsRepository friendsRepository;
        private IFriendRequestsRepository friendRequestRepository;
        private IMessagesRepository messagesRepository;
        private PrimaryMembershipProvider provider;

        public DataManager(IUsersRepository usersRepository, IFriendsRepository friendsRepository, IFriendRequestsRepository friendRequestRepository, IMessagesRepository messagesRepository, PrimaryMembershipProvider provider)
        {
            this.usersRepository = usersRepository;
            this.friendsRepository = friendsRepository;
            this.friendRequestRepository = friendRequestRepository;
            this.messagesRepository = messagesRepository;
            this.provider = provider;
        }

        public IUsersRepository Users { get { return usersRepository; } }
        public IFriendsRepository Friends { get { return friendsRepository; } }
        public IFriendRequestsRepository FriendRequests { get { return friendRequestRepository; } }
        public IMessagesRepository Messages { get { return messagesRepository; } }
        public PrimaryMembershipProvider MembershipProvider { get { return provider; } }
    }
}
