using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Auth0.Core;
using GrubBuddy.Api;

namespace GrubBuddy.DataAccess
{
    public interface IUserRepository
    {
        void LoadUsers();
        bool DoesUserExist(string userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ConcurrentBag<User> _users;
        private readonly IUserApi _userApi;

        public UserRepository(IUserApi userApi)
        {
            _users = _users ?? new ConcurrentBag<User>();
            _userApi = userApi;
        }

        public void LoadUsers()
        {
            var users = _userApi.GetUsers().Result.ToList();
            users?.ForEach(u => _users.Add(u));
        }

        public bool DoesUserExist(string userId)
        {
           return  _users.Any(x => x.UserId.Equals(userId));
        }
    }
}
