using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Entites;
using Movies.API.Models;
using Movies.API.Services;

namespace Movies.API.Controllers
{
    [Route("api/Genres/{genreid}/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesInfoRepository _moviesInfoRepository;
        private readonly IMapper _mapper;
        private new List<string> _allawedExtenstions=new List<string>() { ".jpg",".png"};
        private long _maxAllowedPosterSize = 1048576;
        public MoviesController
            (IMoviesInfoRepository moviesInfoRepository, IMapper mapper)
        {
            _moviesInfoRepository = moviesInfoRepository ?? throw new ArgumentNullException(nameof(moviesInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("getallmovies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies() { 
           
            var moviesForGenere = await _moviesInfoRepository.GetAllMoviesAsync();

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(moviesForGenere));
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable< MovieDto>>> GetMovies(int genreId)
        {
            if (!await _moviesInfoRepository.GenreExistsAsync(genreId))
            {
                return NotFound();
            }
            var moviesForGenere = await _moviesInfoRepository.GetMoviesAsync(genreId);

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(moviesForGenere));
        }
        [HttpGet("{movieid}",Name ="GetMovie")]
        public async Task<ActionResult<MovieDto>> GetMovie(
            int genreId,int movieId )
        {
            if (!await _moviesInfoRepository.GenreExistsAsync(genreId))
            {
                return NotFound();
            }
            var movie = await _moviesInfoRepository.GetMovieAsync(genreId, movieId);
            if(movie == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MovieDto>(movie));
        }


        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(
            int genreId,[FromForm]MovieForCreationDto movie)
        {

            if (!_allawedExtenstions.Contains(Path.GetExtension(movie.Poster.FileName).ToLower()))
            {
                return BadRequest("only .png and .jpg allawed");
            }
            if(movie.Poster.Length > _maxAllowedPosterSize)
            {
                return BadRequest("Max allawed size for poster is 1MB");
            }
            if(!await _moviesInfoRepository.GenreExistsAsync(genreId))
            {
                return NotFound();
            }
            using var dataStream = new MemoryStream();

            await movie.Poster.CopyToAsync(dataStream);

            var finalMovie=_mapper.Map<Entites.Movie>(movie);
            finalMovie.Poster=dataStream.ToArray();
            await _moviesInfoRepository.AddMovieForGenreAsync(genreId,finalMovie);
            await _moviesInfoRepository.SaveChangesAsync();

            var createdMovieToReturn=_mapper.Map<MovieDto>(finalMovie);

            return CreatedAtRoute("GetMovie", new
            {
                genreId= genreId,
                movieId= createdMovieToReturn.Id
            },
            createdMovieToReturn
            );
        }

        

  
    }
}
