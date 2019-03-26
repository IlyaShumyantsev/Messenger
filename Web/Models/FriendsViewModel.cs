using Domain.Entities;
using System.Collections.Generic;

namespace Web.Models
{
    public class FriendsViewModel
    {
        //Друзья
        public IEnumerable<User> Friends { get; set; }

        //Входящие заявки в друзья
        public IEnumerable<User> IncomingRequests { get; set; }

        //Исходящие заявки в друзья
        public IEnumerable<User> OutgoingRequests { get; set; }
    }
}