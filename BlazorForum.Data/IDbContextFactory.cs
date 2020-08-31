using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorForum.Data
{
    /// <summary>
    /// Factory to create new instances of <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="TContext">The type of <seealso cref="DbContext"/> to create.</typeparam>
    public interface IDbContextFactory<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Generate a new <see cref="DbContext"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="TContext"/>.</returns>
        TContext CreateDbContext();
    }
}
