namespace Movies.API.Models
{
    public class GenreDto
    {
        public byte Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public int NumberOfMovie
        {
            get
            {
                return Movie.Count;
            }
        }

        public ICollection<Movie> Movie { get; set; }
              = new List<Movie>();
    }
}
