using DataAccess.Models;

namespace BusinessLogic.Services.Interfaces;

public interface ICoinMarketCapService
{
    Task CreateAsync(CancellationToken cancellationToken = default);
    Task<Coin> GetByNameOrSymbolAsync(string name, CancellationToken cancellationToken = default);
}