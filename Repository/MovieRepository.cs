using Entities;
using Entities.Models;
using Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
//        /movies? name = nombre
//● /movies? genre = idGenero
//● /movies? order = ASC | DESC

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovieByNameAsync(String name)
        {
            return await FindByCondition(movie => movie.Title.Equals(name))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovieByGenderAsync(Guid idGender)
        {
            return await FindByCondition(movie => movie.GenderId.Equals(idGender))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovieOrderAsync(string order)
        {
            IQueryable<Movie>movieSort = from m in FindAll()
                                             select m;

            movieSort = order switch
            {
                "ASC" => movieSort.OrderBy(m => m.Title),
                "DESC" => movieSort.OrderByDescending(m => m.Title),
                _ => movieSort.OrderBy(m => m.Title),
            };
            return await movieSort.ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(Guid movieId)
        {
            return await FindByCondition(movie => movie.Id.Equals(movieId))
                .FirstOrDefaultAsync();
        }
        public async Task<Movie> GetMovieWithDetailsAsync(Guid movieId)
        {
            return await FindByCondition(movie => movie.Id.Equals(movieId))
                .Include(c => c.Characters)
                .FirstOrDefaultAsync();
        }
        public void CreateMovie(Movie movie)
        {
            Create(movie);
        }
        public void UpdateMovie(Movie movie)
        {
            Update(movie);
        }
        public void DeleteMovie(Movie movie)
        {
            Delete(movie);
        }
    }
}