using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TMDBAPI.Models;

namespace TMDBAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }
        public DbSet<FavoriteMovie> FavoriteMovies {get;set;}
    }
}