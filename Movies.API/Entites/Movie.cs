using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.API.Entites
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }

        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public byte[] Poster { get; set; }
        
        [ForeignKey("GenreId")]
        public Genre? Genre { get; set; }
        public byte GenreId { get; set; }


    }
}
