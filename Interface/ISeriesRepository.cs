using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface ISeriesRepository : IRepositoryBase<Series>
    {
        Task<IEnumerable<Series>> GetAllSeriesAsync();
        Task<IEnumerable<Series>> GetSeriesByNameAsync(String name);
        Task<IEnumerable<Series>> GetSeriesByGenderAsync(Guid idGender);
        Task<IEnumerable<Series>> GetSeriesOrderAsync(string order);
        Task<Series> GetSeriesByIdAsync(Guid seriesId);
        Task<Series> GetSeriesWithDetailsAsync(Guid seriesId);
        void CreateSeries(Series series);
        void UpdateSeries(Series series);
        void DeleteSeries(Series series);
    }
}
