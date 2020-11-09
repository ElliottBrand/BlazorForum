using BlazorForum.Models;

namespace BlazorForum.Tests.FakeData
{
    public class FakeUserData
    {
        public static ApplicationUser GetFakeUser(string userId)
        {
            return new ApplicationUser()
            {
                Id = userId,
                UserName = "tester",
                Email = "tester@blazor.net"
            };
        }
    }
}
