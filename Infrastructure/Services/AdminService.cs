using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IReportRepository _reportRepository;
        public AdminService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public Task<IEnumerable<MoviesReportModel>> GetTopPurchasedMovies(DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1)
        {
            return _reportRepository.GetTopPurchasedMovies(fromDate, toDate, pageSize, pageIndex);
        }
    }
}
