using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Entities
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ApplicationDbContext _context;

        public CoinRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Coin coin, CancellationToken cancellationToken = default)
        {
            // await _context.Coins.AddAsync(coin);
            await _context.SaveChangesAsync();
        }
    }
}
