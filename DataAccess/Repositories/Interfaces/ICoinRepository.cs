﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface ICoinRepository
    {
        Task CreateAsync(Coin coin, CancellationToken cancellationToken = default);
    }
}