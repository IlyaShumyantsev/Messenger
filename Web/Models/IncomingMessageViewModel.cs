using Domain.Entities;

namespace Web.Models
{
    public class IncomingMessageViewModel
    {
        //Входящее сообщение
        public IncomingMessage Message { get; set; }

        //Автор
        public User UserForm { get; set; }
    }
}