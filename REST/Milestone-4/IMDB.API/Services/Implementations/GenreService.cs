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
            var genreName = ValidateGenreRequest(request);

            var genre = new Genre
            {
                Name = genreName
            };

            var createdGenre = genreRepository.Create(genre);
            return new GenreResponse
            {
                Id = createdGenre.Id,
                Name = createdGenre.Name
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
                throw new OutOfRangeException("Genre id must be greater than zero.");
            }

            var genre = genreRepository.Get(id);
            if (genre == null)
            {
                throw new NotFoundException("Genre not found.");
            }
            return new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public bool Update(int id, GenreRequest request)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Genre id must be greater than zero.");
            }

            if (genreRepository.Get(id) == null)
            {
                throw new NotFoundException("Genre not found.");
            }

            var genreName = ValidateGenreRequest(request);

            var genre = new Genre
            {
                Id = id,
                Name = genreName
            };

            genreRepository.Update(id, genre);
            return true;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Genre id must be greater than zero.");
            }

            if (genreRepository.Get(id) == null)
            {
                throw new NotFoundException("Genre not found.");
            }

            genreRepository.Delete(id);
            return true;
        }

        private static string ValidateGenreRequest(GenreRequest request)
        {
            if (request == null)
            {
                throw new RequiredFieldException("Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new RequiredFieldException("Genre name is required.");
            }

            return request.Name.Trim();
        }

    }
}
