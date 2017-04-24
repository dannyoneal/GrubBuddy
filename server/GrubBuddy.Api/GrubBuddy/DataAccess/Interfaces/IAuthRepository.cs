using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrubBuddy.DataAccess.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> GetAccessToken();
    }
}
