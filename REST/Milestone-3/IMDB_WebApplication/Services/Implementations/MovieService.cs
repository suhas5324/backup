using AutoMapper;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IProducerRepository producerRepository;
        private readonly IMapper mapper;

        public MovieService(IMovieRepository movieRepository, IProducerRepository producerRepository, IMapper mapper)
        {
            this.movieRepository = movieRepository;
            this.producerRepository = producerRepository;
            this.mapper = mapper;
        }

        public MovieResponse Create(MovieRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.Producer==null)
            {
                return null;
            }

            var movies = movieRepository.Get();
            var movie = mapper.Map<Movie>(request);
            movie.Id = movies.Count == 0 ? 1 : movies.Max(existingMovie => existingMovie.Id) + 1;
            movie.Name = request.Name.Trim();
            movie.Plot = request.Plot?.Trim();
            movie.CoverImage = request.CoverImage?.Trim();
            movie.Producer=request.Producer.Trim();

            movieRepository.Create(movie);
            return mapper.Map<MovieResponse>(movie);
        }

        public IList<MovieResponse> GetAll(int year)
        {
            return movieRepository.GetAll(year).Select(mapper.Map<MovieResponse>).ToList();
        }

        public MovieResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var movie = movieRepository.Get(id);
            return mapper.Map<MovieResponse>(movie);
        }

        public MovieResponse Update(int id, MovieRequest request)
        {
            if (id <= 0 || request == null || string.IsNullOrWhiteSpace(request.Name) || request.Producer==null)
            {
                return null;
            }

            if (movieRepository.Get(id) == null)
            {
                return null;
            }

            var movie = mapper.Map<Movie>(request);
            movie.Id = id;
            movie.Name = request.Name.Trim();
            movie.Plot = request.Plot?.Trim();
            movie.CoverImage = request.CoverImage?.Trim();
            movie.Producer = request.Producer.Trim();

            var updatedMovie = movieRepository.Update(id, movie);
            return mapper.Map<MovieResponse>(updatedMovie);
        }

        public MovieResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var movie = movieRepository.Delete(id);
            return mapper.Map<MovieResponse>(movie);
        }

    }
}
