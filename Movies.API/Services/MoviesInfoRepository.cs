using Microsoft.EntityFrameworkCore;
using Movies.API.DbContexts;
using Movies.API.Entites;


namespace Movies.API.Services
{
    public class MoviesInfoRepository : IMoviesInfoRepository
    {
        private readonly MoviesInfoContext _context;

        public MoviesInfoRepository(MoviesInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> GenreExistsAsync(int genreId)
        {
            return await _context.Genres.AnyAsync(c => c.Id == genreId);

        }

        public void AddGenreAsync(Genre genre)
        {
           _context.Genres.Add(genre);
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await _context.Genres
                .OrderBy(g => g.Name)
                .ToListAsync();
        }
        public async Task<Genre?> GetGenreAsync(int genreId, bool includeMovie)
        {
            if (includeMovie)
            {
                return await _context.Genres
                    .Include(g=>g.movie)
                    .Where(g => g.Id == genreId).FirstOrDefaultAsync();
            }

            return await _context.Genres
                .Where(g => g.Id == genreId).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
        public void DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
        }

        public async Task AddMovieForGenreAsync(int genreId,Movie movie )
        {
            var genre = await GetGenreAsync(genreId, false);
            if (genre != null)
            {
                genre.movie.Add(movie);
            }
        }
        public async Task<Movie?> GetMovieAsync(int genreId, int movieId)
        {
          
            return await _context.Movies
                .Where(m=>m.GenreId==genreId &&m.Id==movieId)
                .FirstOrDefaultAsync();    
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(int genreId)
        {
            return await _context.Movies
                .Where(m => m.GenreId == genreId)
                .OrderByDescending(r => r.Rate).ToListAsync();
        }
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .OrderByDescending(r => r.Rate).ToListAsync();
        }
    }
}
