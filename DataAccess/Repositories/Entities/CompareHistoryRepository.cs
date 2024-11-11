using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Entities
{
    public class CompareHistoryRepository : ICompareHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CompareHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CompareHistory compareData, CancellationToken cancellationToken = default)
        {
            await _context.CompareHistories.AddAsync(compareData, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<CompareHistory>> GetAllAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.CompareHistories.Where(ch => ch.UserId == id).ToListAsync();
        }

        public async Task<CompareHistory?> GetLastAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.CompareHistories.Where(ch => ch.UserId == id).LastOrDefault();
        }
    }
}
