using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _repository;
        public GenreService(IRepository<Genre> repository)
        {
            _repository = repository;
        }
        public async Task<List<GenreModel>> GetAllGenres()
        {
            var genres = await _repository.GetAll();
            List<GenreModel> genresList = new List<GenreModel>();
            foreach (var genre in genres)
            {
                var genreModel = new GenreModel()
                {
                    Name = genre.Name,
                    Id = genre.Id,
                };
                genresList.Add(genreModel);
            }
            return genresList;
        }
    }
}
