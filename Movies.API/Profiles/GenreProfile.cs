using AutoMapper;

namespace Movies.API.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Entites.Genre,Models.GenreDto>();
            CreateMap<Entites.Genre, Models.GenreWithoutMovieDto>();
            CreateMap<Models.GenreForCreationDto, Entites.Genre>();
            CreateMap<Models.GenreForUpdateDto, Entites.Genre>();
        }
    }
}
