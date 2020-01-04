using Microsoft.EntityFrameworkCore;
using MVCMOVIES.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Info> Info { get; set; }

        public DbSet<Operations> Operations { get; set; }
    }
}
