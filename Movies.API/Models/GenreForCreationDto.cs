namespace Movies.API.Models
{
    public class GenreForCreationDto
    {
        [Required(ErrorMessage ="you shoud provide a name value")]
        [MaxLength(100)]
        public string Name { get; set; }=string.Empty;
    }
}
