using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Models;

namespace BlazorForum.Domain.Utilities.Membership
{
    public class Registration
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<Registration> _logger;

        public Registration(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<Registration> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// Adds default roles (Administrator and Contributor) to the database to allow for an easier setup and installation.
        /// </summary>
        /// <returns></returns>
        public async Task AddDefaultRolesAsync()
        {
            try
            {
                await AddRolesHelper("Administrator");
                await AddRolesHelper("Contributor");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task AddRolesHelper(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        /// <summary>
        /// Adds default role to a new user. The very first user will be added as an Administrator, all others start as a Contributor.
        /// </summary>
        /// <returns></returns>

        public async Task AddDefaultUserRole(ApplicationUser user)
        {
            try
            {
                var users = _userManager.Users.ToList();
                var administrators = await _userManager.GetUsersInRoleAsync("Administrator");
                if (users.Count == 1 && administrators.Count == 0)
                    await _userManager.AddToRoleAsync(user, "Administrator");
                else
                    await _userManager.AddToRoleAsync(user, "Contributor");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
