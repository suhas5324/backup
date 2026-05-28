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

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public GenreResponse Create(GenreRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            var genres = genreRepository.Get();
            var genre = new Genre
            {
                Id = genres.Count == 0 ? 1 : genres.Max(existingGenre => existingGenre.Id) + 1,
                Name = request.Name.Trim()
            };

            genreRepository.Create(genre);
            return new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public IList<GenreResponse> Get()
        {
            return genreRepository.Get().Select(g => new GenreResponse
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }

        public GenreResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var genre = genreRepository.Get(id);
            if (genre == null)
            {
                return null;
            }
            return new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
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

            var genre = new Genre
            {
                Id = id,
                Name = request.Name.Trim()
            };

            var updatedGenre = genreRepository.Update(id, genre);
            return new GenreResponse
            {
                Id = updatedGenre.Id,
                Name = updatedGenre.Name
            };
        }

        public GenreResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var genre = genreRepository.Delete(id);
            if (genre == null)
            {
                return null;
            }
            return new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

    }
}
