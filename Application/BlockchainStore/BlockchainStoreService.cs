using Application.Abstractions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application;

public class BlockchainStoreService : IBlockchainStoreService
{
    private readonly IBlockchainRepository _repository;
    private readonly ILogger<IBlockchainStoreService> _logger;

    public BlockchainStoreService(IBlockchainRepository repository, ILogger<BlockchainStoreService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> SaveBlockchain(Blockchain blockchain, CancellationToken token)
    {
        try
        {
            await _repository.Add(blockchain, token);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogWarning($"Error while saving blockchain. Name: {blockchain.Name}." +
                               $" CreatedAt: {blockchain.CreatedAt}.", e);
            return false;
        }
    }

    public async Task<bool> SaveBlockchains(List<Blockchain> blockchains, CancellationToken token)
    {
        try
        {
            await _repository.BulkAdd(blockchains, token);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogWarning($"Error while saving blockchains. Count: {blockchains.Count}.", e);
            return false;
        }
    }
}