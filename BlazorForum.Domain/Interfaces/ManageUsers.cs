using BlazorForum.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageUsers
    {
        Task<ApplicationUser> FindByIdAsync(string userId);
    }

    public class ManageUsers : IManageUsers
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsers(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId) =>
            await _userManager.FindByIdAsync(userId);
    }
}
