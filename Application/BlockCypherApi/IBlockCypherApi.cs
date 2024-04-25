using Domain.Models;

namespace Application.BlockCypherApi;

public interface IBlockCypherApi
{
    Task<Blockchain?> GetBlockChain(BlockchainEndpoint endpoint, CancellationToken token);
}