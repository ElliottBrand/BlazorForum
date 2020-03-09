using System;
using System.Collections.Generic;
using System.Text;
using BlazorForum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ForumPost> ForumPosts { get; set; }

        public DbSet<ForumTopic> ForumTopics { get; set; }

        public DbSet<ForumCategory> ForumCategories { get; set; }

        public DbSet<Forum> Forums { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<Configuration> Configuration { get; set; }

        public DbSet<SitePage> Pages { get; set; }

        public DbSet<UpDownVote> UpDownVotes { get; set; }

        public DbSet<TopicSubscription> TopicSubscriptions { get; set; }
    }
}
