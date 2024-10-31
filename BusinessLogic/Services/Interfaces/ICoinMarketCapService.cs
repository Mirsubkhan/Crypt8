using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces;

public interface ICoinMarketCapService
{
    Task CreateAsync(CancellationToken cancellationToken = default);
    Task<Coin> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}