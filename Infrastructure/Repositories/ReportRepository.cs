using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class ReportRepository : Repository<Movie>, IReportRepository
    {
        public ReportRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        

        public async Task<IEnumerable<MoviesReportModel>> GetTopPurchasedMovies(DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1)
        {
            string connString = _dbContext.Database.GetConnectionString();
            IDbConnection conn = new SqlConnection(connString);
            var procedure = "[usp_GetTopPurchasedMovies]";
            var values = new 
            {
                fromDate = fromDate,
                toDate = toDate,
                pageSize = pageSize,
                pageIndex = pageIndex
            };

            var results = conn.Query(procedure, values, commandType: CommandType.StoredProcedure).ToList();
            List<MoviesReportModel> reports = new List<MoviesReportModel>();
            foreach (var result in results)
            {
                reports.Add(new MoviesReportModel()
                {
                    Title = result.Title,
                    Overview = result.Overview,
                    Tagline = result.Tagline,
                    Budget = result.Budget,
                    Revenue = result.Revenue,
                    ImdbUrl = result.ImdbUrl,
                    TmdbUrl = result.TmdbUrl,
                    PosterUrl = result.PosterUrl,
                    BackdropUrl = result.BackdropUrl,
                    OriginalLanguage = result.OriginalLanguage,
                    ReleaseDate = result.ReleaseDate,
                    RunTime = result.RunTime,
                    Price = result.Price,                   
                });
            }
            return reports;

        }
    }
}
