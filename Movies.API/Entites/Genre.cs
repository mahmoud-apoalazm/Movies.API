
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.API.Entites
{
    public class Genre
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Movie> movie { get; set; }
         = new List<Movie>();


        public Genre(string name)
        {
            Name = name;
        }
    }
}
