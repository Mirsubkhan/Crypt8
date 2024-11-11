using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Entities
{
    class CompareHistoryService : ICompareHistoryService
    {
        private readonly ICompareHistoryRepository _compareHistoryRepository;
        public CompareHistoryService(ICompareHistoryRepository compareHistoryRepository)
        {
            _compareHistoryRepository = compareHistoryRepository;
        }

        public async Task CreateAsync(CompareHistory compareData, CancellationToken cancellationToken = default)
        {
            await _compareHistoryRepository.CreateAsync(compareData, cancellationToken);
        }

        public async Task<IEnumerable<CompareHistory>> GetAllAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var existingHistory = await _compareHistoryRepository.GetAllAsync(id, cancellationToken);
            if (existingHistory == null || !existingHistory.Any())
            {
                return Enumerable.Empty<CompareHistory>();
            }
            return existingHistory;
        }

        public async Task<CompareHistory?> GetLastAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var existingHistory = await _compareHistoryRepository.GetLastAsync(id, cancellationToken);
            if (existingHistory == null)
            {
                return new CompareHistory();
            }
            return existingHistory;
        }
    }
}
