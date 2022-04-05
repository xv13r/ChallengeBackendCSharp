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
    public class SeriesRepository : RepositoryBase<Series>, ISeriesRepository
    {
        public SeriesRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Series>> GetAllSeriesAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<Series>> GetSeriesByNameAsync(String name)
        {
            return await FindByCondition(series => series.Title.Equals(name))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Series>> GetSeriesByGenderAsync(Guid idGender)
        {
            return await FindByCondition(series => series.GenderId.Equals(idGender))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Series>> GetSeriesOrderAsync(string order)
        {
            IQueryable<Series> seriesSort = from m in FindAll()
                                          select m;

            seriesSort = order switch
            {
                "ASC" => seriesSort.OrderBy(m => m.Title),
                "DESC" => seriesSort.OrderByDescending(m => m.Title),
                _ => seriesSort.OrderBy(m => m.Title),
            };
            return await seriesSort.ToListAsync();
        }

        public async Task<Series> GetSeriesByIdAsync(Guid seriesId)
        {
            return await FindByCondition(series => series.Id.Equals(seriesId))
                .FirstOrDefaultAsync();
        }
        public async Task<Series> GetSeriesWithDetailsAsync(Guid seriesId)
        {
            return await FindByCondition(series => series.Id.Equals(seriesId))
                .Include(c => c.Characters)
                .FirstOrDefaultAsync();
        }
        public void CreateSeries(Series series)
        {
            Create(series);
        }
        public void UpdateSeries(Series series)
        {
            Update(series);
        }
        public void DeleteSeries(Series series)
        {
            Delete(series);
        }
    }
}