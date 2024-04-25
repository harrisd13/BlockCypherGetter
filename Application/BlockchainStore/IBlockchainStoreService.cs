using Domain.Models;

namespace Application.Abstractions;

public interface IBlockchainStoreService
{
    Task<bool> SaveBlockchain(Blockchain blockchain, CancellationToken token);

    Task<bool> SaveBlockchains(List<Blockchain> blockchains, CancellationToken token);
}