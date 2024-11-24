using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp
{
    [Serializable]
    internal class User : IUser
    {
        public Guid UserId { get; }
        public string Username { get; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public UserType Type { get; set; }
        public double Balance { get; set; }

        public User(Guid userId, string username, string password, string email, DateTime birthDate, UserType type)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            BirthDate = birthDate;
            Type = type;
            Balance = 0;
        }

    }
}