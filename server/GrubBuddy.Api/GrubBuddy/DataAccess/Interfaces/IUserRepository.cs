using System;
using System.Collections.Generic;
using System.Text;

namespace GrubBuddy.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        void LoadUsers();
        bool DoesUserExist(string userId);
    }
}
