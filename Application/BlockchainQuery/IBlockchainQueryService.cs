﻿using Domain.Models;

namespace Application.BlockchainQuery;

public interface IBlockchainQueryService
{
    Task<IEnumerable<BlockchainBase>> ListBlockchains(string name, DateTime? from, DateTime? to, int? limit, CancellationToken token);

    bool IsValidName(string name);
}