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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }

        public GenreResponse Create(GenreRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

           
            var genre = mapper.Map<Genre>(request);
           
            genre.Name = request.Name.Trim();

            genreRepository.Create(genre);
            var genres = genreRepository.Get();
            genre.Id = genres.Count == 0 ? 1 : genres.Max(existingGenre => existingGenre.Id);
            return mapper.Map<GenreResponse>(genre);
        }

        public IList<GenreResponse> Get()
        {
            return mapper.Map<IList<GenreResponse>>(genreRepository.Get());
        }

        public GenreResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var genre = genreRepository.Get(id);
            return mapper.Map<GenreResponse>(genre);
        }

        public GenreResponse Update(int id, GenreRequest request)
        {
            if (id <= 0 || request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            if (genreRepository.Get(id) == null)
            {
                return null;
            }

            var genre = mapper.Map<Genre>(request);
            genre.Id = id;
            genre.Name = request.Name.Trim();

            return mapper.Map<GenreResponse>(genreRepository.Update(id, genre));
        }

        public GenreResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var genre = genreRepository.Delete(id);
            return mapper.Map<GenreResponse>(genre);
        }

    }
}
