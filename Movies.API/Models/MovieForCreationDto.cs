namespace Movies.API.Models
{
    public class MovieForCreationDto
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }

        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public IFormFile Poster { get; set; }

    
    }
}
