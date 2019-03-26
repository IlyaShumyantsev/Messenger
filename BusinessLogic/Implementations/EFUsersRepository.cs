using BusinessLogic.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;

namespace BusinessLogic.Implementations
{
    public class EFUsersRepository : IUsersRepository
    {
        private EFDbContext context;
        public EFUsersRepository(EFDbContext context)
        {
            this.context = context;
        }

        public void CreateUser(string userName, string password, string email, string firstName, string surname)
        {
            User user = new User
            {
                Username = userName,
                Email = email,
                Password = password,
                CreatedDate = DateTime.Now,
                FirstName = firstName,
                Surname = surname
            };
            SaveUser(user);
        }

        public MembershipUser GetMembershipUserByName(string userName)
        {
            User user = context.Users.FirstOrDefault(x => x.Username == userName);
            if(user != null)
            {
                return new MembershipUser(
                    "CustomMembershipProvider",
                    user.Username,
                    user.Id,
                    user.Email,
                    "",
                    null,
                    true,
                    false,
                    user.CreatedDate,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now);
            }
            return null;
        }

        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByName(string userName)
        {
            return context.Users.FirstOrDefault(x => x.Username == userName);
        }

        public string GetUserNameByEmail(string email)
        {
            User user = context.Users.FirstOrDefault(x => x.Email == email);
            return user != null ? user.Username : "";
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }

        public void SaveUser(User user)
        {
            if(user.Id == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                context.Entry(user).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public bool ValidateUser(string userName, string password)
        {
            User user = context.Users.FirstOrDefault(x => x.Username == userName);
            if (user != null && user.Password == password)
            {
                return true;
            }
            return false;
        }
    }
}
