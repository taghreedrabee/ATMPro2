using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp
{
    internal interface IUser
    {
        Guid UserId { get; }
        string Username { get; }
        string Password { get; set; }
        string Email { get; set; }
        DateTime BirthDate { get; set; }
        UserType Type { get; set; }
        double Balance { get; set; }
    }
    [Serializable]
    public enum UserType
    {
        Ordinary,
        VIP
    }
}
