﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp
{
    internal interface IUserRepository
    {
        void AddUser(IUser user);
        IUser GetUserByUsername(string username);
        IUser GetUserById(Guid userId);
        void UpdateUser(IUser user);
        bool UserExists(string username);
        bool DeleteUser(string username, string password);
    }

    
}
