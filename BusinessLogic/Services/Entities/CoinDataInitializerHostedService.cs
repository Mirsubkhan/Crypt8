﻿using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusinessLogic.Services.Entities;

public class CoinDataInitializerHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public CoinDataInitializerHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var coinService = scope.ServiceProvider.GetRequiredService<ICoinMarketCapService>();
            var coinRepository = scope.ServiceProvider.GetRequiredService<ICoinRepository>();
            
            var existingCoins = await coinRepository.GetAllAsync(cancellationToken);
            if (existingCoins == null || !existingCoins.Any())
            {
                await coinService.CreateAsync(cancellationToken);
            }
            else
            {
                Console.WriteLine($"Coins already exist in the database: {existingCoins.Count()}");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
