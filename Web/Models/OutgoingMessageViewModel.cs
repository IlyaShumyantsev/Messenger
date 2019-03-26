using Domain.Entities;

namespace Web.Models
{
    public class OutgoingMessageViewModel
    {
        //исходящее сообщение
        public OutgoingMessage Message { get; set; }

        //Адресат
        public User UserTo { get; set; }
    }
}