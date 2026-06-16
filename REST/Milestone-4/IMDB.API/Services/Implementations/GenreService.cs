using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreResponse> CreateAsync(GenreRequest request)
        {
            var genreName = ValidateGenreRequest(request);

            var genre = new Genre
            {
                Name = genreName
            };

            var createdGenre = await _genreRepository.CreateAsync(genre);
            return new GenreResponse
            {
                Id = createdGenre.Id,
                Name = createdGenre.Name
            };
        }

        public async Task<IList<GenreResponse>> GetAsync()
        {
            var genres = await _genreRepository.GetAsync();

            return genres.Select(g => new GenreResponse
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }

        public async Task<GenreResponse> GetAsync(int id)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Genre id must be greater than zero.");
            }

            var genre = await _genreRepository.GetAsync(id);
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

        public async Task UpdateAsync(int id, GenreRequest request)
        {
            var genreName = await ValidateGenreUpdateAsync(id, request);

            var genre = new Genre
            {
                Id = id,
                Name = genreName
            };

            await _genreRepository.UpdateAsync(genre);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Genre id must be greater than zero.");
            }

            if (await _genreRepository.GetAsync(id) == null)
            {
                throw new NotFoundException("Genre not found.");
            }

            await _genreRepository.DeleteAsync(id);
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

        private async Task<string> ValidateGenreUpdateAsync(int id, GenreRequest request)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Genre id must be greater than zero.");
            }

            if (await _genreRepository.GetAsync(id) == null)
            {
                throw new NotFoundException("Genre not found.");
            }

            return ValidateGenreRequest(request);
        }
    }
}
