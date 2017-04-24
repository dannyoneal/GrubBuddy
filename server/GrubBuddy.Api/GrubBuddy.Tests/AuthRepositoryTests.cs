using System.Threading.Tasks;
using Moq;
using Xunit;
using GrubBuddy.Api;
using GrubBuddy.DataAccess.Auth0;
using GrubBuddy.Models;
using GrubBuddy.DataAccess.Interfaces;

namespace GrubBuddy.Tests
{
    public class AuthRepositoryTests
    {
        private class TestPackage
        {
            public readonly Mock<IAuth0Api> ApiMock;
            public readonly IAuthRepository Repo;
            public TestPackage()
            {
                ApiMock = new Mock<IAuth0Api>();
                Repo = new Auth0Repository(ApiMock.Object);
            }

        }
        
        [Fact]
        public void GetAccessToken_CallsApiWithNoCurrentToken()
        {
            var pkg = new TestPackage();
            pkg.ApiMock.Setup(x => x.GetAccessToken()).Returns(new Task<AccessToken>(() => null));

            pkg.Repo.GetAccessToken();

            pkg.ApiMock.Verify(x => x.GetAccessToken(), Times.AtMostOnce);
        }

        [Fact]
        public async void GetAccessToken_DoesNotCallApiWithCurrentToken()
        {
            var pkg = new TestPackage();
            pkg.ApiMock.Setup(x => x.GetAccessToken()).Returns(Task.FromResult(new AccessToken
            {
                Token = "123",
                ExpiresIn = 100
            }));

            await pkg.Repo.GetAccessToken();

            await pkg.Repo.GetAccessToken();

            pkg.ApiMock.Verify(x => x.GetAccessToken(), Times.AtMostOnce);
        }

        [Fact]
        public async void GetAccessToken_CallsApiWithExpiredToken()
        {
            var pkg = new TestPackage();
            pkg.ApiMock.Setup(x => x.GetAccessToken()).Returns(Task.FromResult(new AccessToken
            {
                Token = "123",
                ExpiresIn = 0
            }));

            await pkg.Repo.GetAccessToken();
            

            await pkg.Repo.GetAccessToken();

            pkg.ApiMock.Verify(x => x.GetAccessToken(), Times.AtMost(2));
        }
    }
}
