using AutoMapper;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;

namespace IMDB_WebApplication.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<ActorRequest, Actor>().ReverseMap();
            CreateMap<Actor, ActorResponse>().ReverseMap();

            CreateMap<SignupRequest, User>().ReverseMap();

            CreateMap<ProducerRequest, Producer>().ReverseMap();
            CreateMap<Producer,ProducerResponse>().ReverseMap();

            CreateMap<GenreRequest,Genre>().ReverseMap();
            CreateMap<Genre,GenreResponse>().ReverseMap();

            CreateMap<MovieRequest,Movie>().ReverseMap();
            CreateMap<Movie,MovieResponse>().ReverseMap();

            CreateMap<ReviewRequest, Review>().ReverseMap();
            CreateMap<Review, ReviewResponse>().ReverseMap();
        }
    }
}
