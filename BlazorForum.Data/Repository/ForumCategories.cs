using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Data.Repository
{
    public class ForumCategories
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ForumCategories(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<ForumCategory>> GetForumCategoriesAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumCategories.ToListAsync();
        }

        public async Task<List<ForumCategory>> GetForumCategoriesAsync(int forumId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumCategories.Where(p => p.ForumId == forumId).ToListAsync();
        }

        public async Task<ForumCategory> GetForumCategory(int categoryId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumCategories.Where(p => p.ForumCategoryId == categoryId).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateCategoryAsync(ForumCategory newCategory)
        {
            using var context = _dbFactory.CreateDbContext();
            var categories = context.ForumCategories;
            await categories.AddAsync(newCategory);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(ForumCategory editedCategory)
        {
            using var context = _dbFactory.CreateDbContext();
            var category = await context.ForumCategories.Where(p => p.ForumCategoryId == editedCategory.ForumCategoryId).FirstOrDefaultAsync();
            if(category != null)
            {
                category.Title = editedCategory.Title;
                category.Description = editedCategory.Description;
                category.ForumId = editedCategory.ForumId;
                category.Rank = editedCategory.Rank;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            using var context = _dbFactory.CreateDbContext();
            var categories = context.ForumCategories;
            var category = await categories.Where(p => p.ForumCategoryId == categoryId).FirstOrDefaultAsync();
            if(category != null)
            {
                categories.Remove(category);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
