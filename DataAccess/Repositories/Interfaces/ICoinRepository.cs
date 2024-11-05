using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface ICoinRepository
    {
        Task CreateAsync(Coin coin, CancellationToken cancellationToken = default);
        Task<Coin?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Coin>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
