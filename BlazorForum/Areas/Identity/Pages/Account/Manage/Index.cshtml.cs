using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlazorForum.Models;
using BlazorForum.Domain.Interfaces;
using System.Text.RegularExpressions;
using System.Web;

namespace BlazorForum.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IManageUsers _manageUsers;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IManageUsers manageUsers)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _manageUsers = manageUsers;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ProfileInputModel Input { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            var appUser = await _manageUsers.GetUserAsync(user.Id);

            Username = appUser.UserName;

            Input = new ProfileInputModel
            {
                DisplayName = appUser.DisplayName,
                Title = appUser.Title,
                Location = appUser.Location,
                About = appUser.About,
                PhoneNumber = appUser.PhoneNumber,
                GitHub = appUser.GitHub,
                Twitter = appUser.Twitter,
                LinkedIn = appUser.LinkedIn,
                Website = appUser.Website
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // Probably don't need this, but remove any scripts that may have somehow been added, just in case.
            Input.About = Regex.Replace(Input.About, @"<script[^>]*>[\s\S]*?</script>", string.Empty);

            user.DisplayName = Input.DisplayName;
            user.Title = Input.Title;
            user.Location = Input.Location;
            user.About = Input.About;
            user.GitHub = Input.GitHub;
            user.Twitter = Input.Twitter;
            user.LinkedIn = Input.LinkedIn;
            user.Website = Input.Website;
            user.PhoneNumber = Input.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new InvalidOperationException($"Unexpected error occurred updating the profile.");

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
