using BusinessLogic;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private DataManager dataManager;
        public AccountController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(dataManager.MembershipProvider.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Не удалось войти");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus status = dataManager.MembershipProvider.CreateUser(model.UserName, model.Password, model.Email, model.FirstName, model.Surname);
                if(status == MembershipCreateStatus.Success)
                {
                    return View("Success");
                }
                ModelState.AddModelError("", GetMembershipCreateStatusResultText(status));
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        //Создание текста для ошибки MembershipCreateStatus
        public string GetMembershipCreateStatusResultText(MembershipCreateStatus status)
        {
            if(status == MembershipCreateStatus.DuplicateEmail)
            {
                return "На данный email уже зарегистрирован аккаунт";
            }
            else if (status == MembershipCreateStatus.DuplicateUserName)
            {
                return "Пользователь с такоим логином уже зарегистрирован";
            }
            else
            {
                return "Неизвестная ошибка";
            }
        }
    }
}