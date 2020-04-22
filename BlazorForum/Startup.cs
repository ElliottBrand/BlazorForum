using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorForum.Areas.Identity;
using BlazorForum.Data;
using BlazorForum.Models;
using BlazorForum.Domain.Interfaces;
using BlazorForum.Pages.Components.BlazorModal;
using Microsoft.AspNetCore.Identity.UI.Services;
using BlazorForum.Domain.Services;
using Microsoft.AspNetCore.Http;
using BlazorForum.Pages.Components.Head;

namespace BlazorForum
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Added for sending email through SendGrid
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddHttpContextAccessor();
            services.AddTransient<IManageForums, ManageForums>();
            services.AddTransient<IManageForumCategories, ManageForumCategories>();
            services.AddTransient<IManageForumTopics, ManageForumTopics>();
            services.AddTransient<IManageForumPosts, ManageForumPosts>();
            services.AddTransient<IManageThemes, ManageThemes>();
            services.AddTransient<IManageConfiguration, ManageConfiguration>();
            services.AddTransient<IManagePages, ManagePages>();
            services.AddTransient<IManageUpDownVotes, ManageUpDownVotes>();
            services.AddTransient<IManageTopicSubscriptions, ManageTopicSubscriptions>();
            services.AddBlazorModal();
            services.AddHeadBuilder();

            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapBlazorHub("~/admin/_blazor");
                endpoints.MapFallbackToAreaPage("~/admin/{*clientroutes:nonfile}", "/_AdminHost", "Admin");
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapRazorPages();
            });
        }
    }
}
