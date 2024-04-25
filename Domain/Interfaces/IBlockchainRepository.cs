using Domain.Models;

namespace Domain.Interfaces;

public interface IBlockchainRepository
{
    Task<List<BlockchainBase>> List(string name, DateTime? from, DateTime? to, int? limit, CancellationToken token);

    Task Add(Blockchain blockchain, CancellationToken token);

    Task BulkAdd(List<Blockchain> blockchains, CancellationToken token);
}