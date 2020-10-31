using BlazorForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
