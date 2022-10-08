using AutoMapper;

namespace Movies.API.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() {

            CreateMap<Entites.Movie, Models.MovieDto>();
            CreateMap<Models.MovieForCreationDto, Entites.Movie>().ForMember(src => src.Poster, opt => opt.Ignore());

        }
    }
}
