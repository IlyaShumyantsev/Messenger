using BusinessLogic;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private DataManager dataManager;
        public MessagesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            //Подготавливаем все входящие сообщения
            List<IncomingMessageViewModel> model = new List<IncomingMessageViewModel>();
            foreach(IncomingMessage message in dataManager.Messages.GetIncomingMessagesByUserId((int)Membership.GetUser().ProviderUserKey))
            {
                model.Add(new IncomingMessageViewModel
                {
                    Message = message,
                    UserForm = dataManager.Users.GetUserById(message.UserFromId)
                });
            }

            return View(model);
        }

        public ActionResult Outgoing()
        {
            //Подготавливаем все исходящие сообщения и адресатов
            List<OutgoingMessageViewModel> model = new List<OutgoingMessageViewModel>();
            foreach(OutgoingMessage message in dataManager.Messages.GetOutgoingMessagesByUserId((int)Membership.GetUser().ProviderUserKey))
            {
                model.Add(new OutgoingMessageViewModel
                {
                    Message = message,
                    UserTo = dataManager.Users.GetUserById(message.UserToId)
                });
            }

            return View(model);
        }

        public ActionResult NewMessage(int userToId)
        {
            //Перед отправкой определяем автора, дату и адресата
            var message = new OutgoingMessage
            {
                UserId = (int)Membership.GetUser().ProviderUserKey,
                UserToId = userToId,
                CreateDate = DateTime.Now
            };

            return View(message);
        }

        [HttpPost]
        public ActionResult NewMessage(OutgoingMessage message)
        {
            if (ModelState.IsValid)
            {
                //После сохранения исходящего сообщения создаем соответствующее
                //входящее сообщение для другого пользователя
                dataManager.Messages.SaveOutgoingMessage(message);
                dataManager.Messages.SaveIncomingMessage(new IncomingMessage
                {
                    UserId = message.UserToId,
                    UserFromId = message.UserId,
                    CreateDate = DateTime.Now,
                    Text = message.Text
                });
                return RedirectToAction("Index", "Home");
            }

            return View(message);
        }
    }
}