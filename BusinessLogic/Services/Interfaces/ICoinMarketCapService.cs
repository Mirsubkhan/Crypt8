using DataAccess.Models;

namespace BusinessLogic.Services.Interfaces;

public interface ICoinMarketCapService
{
    Task CreateAsync(CancellationToken cancellationToken = default);
    Task<Coin> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Coin?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default);
}