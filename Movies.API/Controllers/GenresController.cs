using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Entites;
using Movies.API.Models;
using Movies.API.Services;

namespace Movies.API.Controllers
{
    [Route("api/Genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMoviesInfoRepository _moviesInfoRepository;
        private readonly IMapper _mapper;

        public GenresController
            (IMoviesInfoRepository moviesInfoRepository,
            IMapper mapper
            )
        {
            _moviesInfoRepository = moviesInfoRepository ?? throw new ArgumentNullException(nameof(moviesInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreWithoutMovieDto>>> GetGenres()
        {
            var genresEntities=await  _moviesInfoRepository.GetGenresAsync();
            return Ok(_mapper.Map<IEnumerable<GenreWithoutMovieDto>>(genresEntities));
        }




        [HttpGet("{Genreid}", Name = "GetGenre")]
        public async Task<IActionResult> GetGenre(int genreId, bool includeMovie=false)
        {
            var genre = await _moviesInfoRepository.GetGenreAsync(genreId,includeMovie);
              if (genre == null)
              {
              return NotFound();
              }
            if (includeMovie)
            {
                return Ok(_mapper.Map<GenreDto>(genre));
            }
            return Ok(_mapper.Map<GenreWithoutMovieDto>(genre));
        }




        [HttpPost]
        public async Task<ActionResult<GenreWithoutMovieDto>> CreateGenre(GenreForCreationDto genre)
        {
            var finalGenre = _mapper.Map<Entites.Genre>(genre);
             _moviesInfoRepository.AddGenreAsync(finalGenre);
            await _moviesInfoRepository.SaveChangesAsync();

            var createdGenreToReturn=_mapper.Map<GenreWithoutMovieDto>(finalGenre);

            return CreatedAtRoute("GetGenre", new
            {
                genreId= createdGenreToReturn.Id

            },
            createdGenreToReturn
            );
        }



        [HttpPut("{genreid}")]
        public async Task<ActionResult> UpdateGenre(int genreId,
            GenreForUpdateDto genre
            )
        {
            if (!await _moviesInfoRepository.GenreExistsAsync(genreId))
            {
                return NotFound($"NoGenre Was Found With Id:{genreId}");
            }
            var genreEntity = await _moviesInfoRepository.GetGenreAsync(genreId,false);
            _mapper.Map(genre, genreEntity);
            await _moviesInfoRepository.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{genreid}")]
        public async Task<ActionResult> DeleteGenre(int genreId)
        {
            if (!await _moviesInfoRepository.GenreExistsAsync(genreId))
            {
                return NotFound($"NoGenre Was Found With Id:{genreId}");
            }
            var genreEntity = await _moviesInfoRepository.GetGenreAsync(genreId,false);
            _moviesInfoRepository.DeleteGenre(genreEntity!);
            await _moviesInfoRepository.SaveChangesAsync();

            return NoContent();


        }

    }
}
