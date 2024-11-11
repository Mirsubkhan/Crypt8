using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICompareHistoryService
    {
        Task CreateAsync(CompareHistory compareData, CancellationToken cancellationToken = default);
        Task<IEnumerable<CompareHistory>> GetAllAsync(Guid id, CancellationToken cancellationToken = default);
        Task<CompareHistory?> GetLastAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
