using BusinessLogic;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DataManager dataManager;
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index(int id = 0)
        {
            //Если в url не был представлен id пользователя, то явным образом вычисляем этот id
            //перенаправляем пользователя на то же действие, но добавляем в адрес вычесленный id
            if(id == 0)
            {
                return RedirectToAction("Index", new { id = Membership.GetUser().ProviderUserKey });
            }
            User user = dataManager.Users.GetUserById(id);

            //Исходя из значения id в url, определяем на чью страницу мы попали
            UserViewModel userViewModel = new UserViewModel
            {
                User = user,
                UserIsMe = id == (int)Membership.GetUser().ProviderUserKey,
                UserIsMyFriend = dataManager.Friends.UsersAreFriends((int)Membership.GetUser().ProviderUserKey, user.Id),
                FriendRequestIsSent = dataManager.FriendRequests.RequestIsSent(user.Id, (int)Membership.GetUser().ProviderUserKey)
            };

            ////////////////////////////////////////////////////////////////////////////
            int currentUserId = (int)Membership.GetUser().ProviderUserKey;
            FriendsViewModel friendsViewModel= new FriendsViewModel();
            IEnumerable<User> allUsers = dataManager.Users.GetUsers();
            IEnumerable<Friend> allFriends = dataManager.Friends.GetFriends().Where(x => x.FriendId == currentUserId);
            IEnumerable<FriendRequest> allRequests = dataManager.FriendRequests.GetFriendRequests().Where(x => x.UserId == currentUserId || x.PossibleFriendId == currentUserId);
            IEnumerable<FriendRequest> incomingReuqests = allRequests.Where(x => x.UserId == currentUserId);

            friendsViewModel.IncomingRequests = (from aU in allUsers
                                      from iR in incomingReuqests
                                      where aU.Id == iR.PossibleFriendId
                                      select aU);

            friendsViewModel.Friends = (from aU in allUsers
                                        from aF in allFriends
                                        where aU.Id == aF.UserId
                                        select aU);


            ///////////////////////////////////////////////////////////////////
            var modelCollection = new ModelCollection();
            modelCollection.AddModel(userViewModel);
            modelCollection.AddModel(friendsViewModel);
            return View(modelCollection);
            //return View(model);
        }
    }
}
