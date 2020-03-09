using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BlazorForum.Models;
using BlazorForum.Domain.Interfaces;
using BlazorForum.Domain.Helpers.Membership;

namespace BlazorForum.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IManageForumTopics _manageForumTopics;
        private readonly IManageForumPosts _manageForumPosts;
        private readonly IManageUpDownVotes _manageUpDownVotes;
        private readonly IManageTopicSubscriptions _manageTopicSubscriptions;

        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IManageForumTopics manageForumTopics,
            IManageForumPosts manageForumposts,
            IManageUpDownVotes manageUpDownVotes,
            IManageTopicSubscriptions manageTopicSubscriptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _manageForumTopics = manageForumTopics;
            _manageForumPosts = manageForumposts;
            _manageUpDownVotes = manageUpDownVotes;
            _manageTopicSubscriptions = manageTopicSubscriptions;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            // Mark Topics and Posts as deleted, plus remove all UpDownVotes by user
            var userDbFilesCleanedUp = await new UserDeleteHelpers(_manageForumTopics, _manageForumPosts, 
                _manageUpDownVotes, _manageTopicSubscriptions)
                .CleanupUserData(user.Id);

            IdentityResult result = null;
            if(userDbFilesCleanedUp)
                result = await _userManager.DeleteAsync(user);

            var userId = await _userManager.GetUserIdAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
