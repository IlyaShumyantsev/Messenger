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
    public class FriendsController : Controller
    {
        private DataManager dataManager;
        public FriendsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            int currentUserId = (int)Membership.GetUser().ProviderUserKey;
            FriendsViewModel model = new FriendsViewModel();

            //Выбираем всех пользователей
            IEnumerable<User> allUsers = dataManager.Users.GetUsers();

            //ВЫбираем все заявки в друзья(входящие и исходящие)
            IEnumerable<FriendRequest> allRequests = dataManager.FriendRequests.GetFriendRequests().Where(x => x.UserId == currentUserId || x.PossibleFriendId == currentUserId);

            //Из всех заявок выбираем входящие
            IEnumerable<FriendRequest> incomingReuqests = allRequests.Where(x => x.UserId == currentUserId);

            //Из всех заявок выбираем исходящие
            IEnumerable<FriendRequest> outgoingRequests = allRequests.Where(x => x.PossibleFriendId == currentUserId);

            //По Id из входящих заявок выбираем соотвутствующих user и передаем их в модель
            model.IncomingRequests = (from aU in allUsers
                                      from iR in incomingReuqests
                                      where aU.Id == iR.PossibleFriendId
                                      select aU);

            //То же и с исходящими заявками
            model.OutgoingRequests = (from aU in allUsers
                                      from iR in outgoingRequests
                                      where aU.Id == iR.UserId
                                      select aU);

            //Выбираем все записи о друзьях для пользователя, кто сейчас залогинен
            IEnumerable<Friend> allFriends = dataManager.Friends.GetFriends().Where(x => x.FriendId == currentUserId);

            //По id из записей о друзьях выбираем соответствующих user и передаем в модель
            model.Friends = (from aU in allUsers
                             from aF in allFriends
                             where aU.Id == aF.UserId
                             select aU);

            var modelCollection = new ModelCollection();
            modelCollection.AddModel(model);
            return View(modelCollection);
        }

        //Пользователь подает заявку в друзья
        public ActionResult AddFriendRequest(int id)
        {
            FriendRequest friendRequest = new FriendRequest
            {
                UserId = id,
                PossibleFriendId = (int)Membership.GetUser().ProviderUserKey
            };

            dataManager.FriendRequests.AddFriendRequest(friendRequest);

            return RedirectToAction("Index", "Home", new { id });
        }

        //Отмена своей заявки в друзья
        public ActionResult CancelFriendRequest(int id)
        {
            dataManager.FriendRequests.DeleteFriendRequest(dataManager.FriendRequests.GetFriendRequests().FirstOrDefault(x => x.UserId == id && x.PossibleFriendId == (int)Membership.GetUser().ProviderUserKey));
            return RedirectToAction("Index", "Home", new { id });
        }

        //Отклонить заявку в друзья
        public ActionResult DisagreeFriendRequest(int id)
        {
            dataManager.FriendRequests.DeleteFriendRequest(dataManager.FriendRequests.GetFriendRequests().FirstOrDefault(x => x.UserId == (int)Membership.GetUser().ProviderUserKey && x.PossibleFriendId == id));
            return RedirectToAction("Index", "Home", new { id });
        }

        //Пользователь принял заявку в друзья
        public ActionResult ConfirmFriendRequest(int id)
        {
            //Создаем дублирующую запись в отношениях "друзья" 2-х пользователей
            Friend friend1 = new Friend
            {
                UserId = (int)Membership.GetUser().ProviderUserKey,
                FriendId = id
            };

            Friend friend2 = new Friend
            {
                UserId = id,
                FriendId = (int)Membership.GetUser().ProviderUserKey
            };

            dataManager.Friends.AddFriend(friend1);
            dataManager.Friends.AddFriend(friend2);

            //Удаляем соответствующую заявку
            dataManager.FriendRequests.DeleteFriendRequest(dataManager.FriendRequests.GetFriendRequests().FirstOrDefault(x => (x.UserId == id && x.PossibleFriendId == (int)Membership.GetUser().ProviderUserKey) || (x.UserId == (int)Membership.GetUser().ProviderUserKey && x.PossibleFriendId == id)));
            return RedirectToAction("Index", "Home", new { id });
        }

        //Пользователь удалил другого пользователя из друзей
        public ActionResult DeleteFriend(int id)
        {
            //Удалим обе дублирующие записи, чтобы сохранить целостность данных
            dataManager.Friends.DeleteFriend(dataManager.Friends.GetFriends().FirstOrDefault(x => x.UserId == id && x.FriendId == (int)Membership.GetUser().ProviderUserKey));
            dataManager.Friends.DeleteFriend(dataManager.Friends.GetFriends().FirstOrDefault(x => x.UserId == (int)Membership.GetUser().ProviderUserKey && x.FriendId == id));
            return RedirectToAction("Index", "Home", new { id });
        }
    }
}