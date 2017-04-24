using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Auth0.AuthenticationApi.Models;
using Auth0.Core;
using Auth0.ManagementApi;
using GrubBuddy.DataAccess;
using GrubBuddy.Models;
using MongoDB.Bson;
using Rest;
using GrubBuddy.DataAccess.Interfaces;

namespace GrubBuddy.Api
{
    public interface IUserApi
    {
        Task<IList<User>> GetUsers();
    }
    public class UserApi : IUserApi
    {
        private readonly IManagementApiClient _managementApi;
        public UserApi(IAuthRepository authRepo, string url)
        {
            _managementApi = new ManagementApiClient(authRepo.GetAccessToken().Result, url);
        }
        public async Task<IList<User>> GetUsers()
        {
            return await _managementApi.Users.GetAllAsync();
        }



    }
}
