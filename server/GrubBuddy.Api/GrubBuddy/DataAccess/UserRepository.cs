using System.Collections.Concurrent;
using System.Linq;
using Auth0.Core;
using GrubBuddy.Api;

namespace GrubBuddy.DataAccess
{
    public interface IUserRepository
    {
        void LoadUsers();
    }

    public class UserRepository : IUserRepository
    {
        private readonly ConcurrentBag<User> _users;
        private readonly IUserApi _userApi;

        public UserRepository(IUserApi userApi)
        {
            _users = new ConcurrentBag<User>();
            _userApi = userApi;
        }

        public void LoadUsers()
        {
            var users = _userApi.GetUsers().Result.ToList();
            users?.ForEach(u => _users.Add(u));
        }
    }
}
