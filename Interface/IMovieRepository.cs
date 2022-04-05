using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<IEnumerable<Movie>> GetMovieByNameAsync(String name);
        Task<IEnumerable<Movie>> GetMovieByGenderAsync(Guid idGender);
        Task<IEnumerable<Movie>> GetMovieOrderAsync(string order);
        Task<Movie> GetMovieByIdAsync(Guid movieId);
        Task<Movie> GetMovieWithDetailsAsync(Guid movieId);
        void CreateMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(Movie movie);
    }
}
