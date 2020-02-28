using BlazorForum.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlazorForum.Models;

namespace BlazorForum.Domain.Utilities.Membership
{
    public class Email
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IManageConfiguration _config;

        public Email(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IManageConfiguration config)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _config = config;
        }

        /// <summary>
        /// Builds and sends registration confirmation email.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="user"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>

        public async Task BuildSendRegistrationConfirmation(string emailAddress, ApplicationUser user, PageModel pageModel)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = pageModel.Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: pageModel.Request.Scheme);

            var configuration = await _config.GetConfigAsync();
            var prependedConfirmationMsg = !String.IsNullOrEmpty(configuration.RegistrationApprovalMessage)
                ? configuration.RegistrationApprovalMessage : "";

            await _emailSender.SendEmailAsync(
                emailAddress,
                "Confirm your email",
                $"{prependedConfirmationMsg}Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }
    }
}
