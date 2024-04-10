using IdentityServer3.Core.Services;
using Moq;

namespace MindSpringTest
{
    public class UnitTest1
    {
        private readonly Mock<IUserService> _userServiceMock;

        public UserControllerTests() => _userServiceMock = new Mock<IUserService>();
        [Fact]
        public void Test1()
        {

        }
    }
}