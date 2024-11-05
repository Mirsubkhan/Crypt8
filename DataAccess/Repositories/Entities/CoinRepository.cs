using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repositories.Entities;

public class CoinRepository : ICoinRepository
{
    private readonly ApplicationDbContext _context;

    public CoinRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Coin coin, CancellationToken cancellationToken = default)
    {
        await _context.Coins.AddAsync(coin);
        await _context.SaveChangesAsync();
    }

    public async Task<Coin?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Coins.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<IEnumerable<Coin>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Coins.ToListAsync(cancellationToken);
    }
}
