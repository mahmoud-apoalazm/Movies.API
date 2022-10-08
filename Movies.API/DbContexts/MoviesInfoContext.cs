using Microsoft.EntityFrameworkCore;
using Movies.API.Entites;

namespace Movies.API.DbContexts
{
    public class MoviesInfoContext :DbContext
    {
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public MoviesInfoContext(DbContextOptions<MoviesInfoContext>options)
           :base(options)
        {
                
        }
    }
}
