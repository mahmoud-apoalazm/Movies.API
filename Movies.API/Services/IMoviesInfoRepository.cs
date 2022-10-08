using Movies.API.Entites;

namespace Movies.API.Services
{
    public interface IMoviesInfoRepository
    {
        Task<IEnumerable<Genre>> GetGenresAsync();
        public Task<bool> GenreExistsAsync(int genreId);
        Task<Genre?> GetGenreAsync(int genreId, bool includeMovie);
        void AddGenreAsync(Genre genre);
        public void DeleteGenre(Genre genre);
        Task<bool> SaveChangesAsync();
        public  Task<Movie?> GetMovieAsync(int genreId, int movieId);

        public Task AddMovieForGenreAsync(int genreId, Movie movie);
        public  Task<IEnumerable<Movie>> GetMoviesAsync(int genreId);
        public  Task<IEnumerable<Movie>> GetAllMoviesAsync();
    }
}
