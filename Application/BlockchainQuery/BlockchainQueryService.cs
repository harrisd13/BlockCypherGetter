using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Application.BlockchainQuery;

public class BlockchainQueryService : IBlockchainQueryService
{
    private readonly IBlockchainRepository _repository;
    private readonly List<string?> _endpointNames;

    public BlockchainQueryService(IBlockchainRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _endpointNames = configuration.GetSection("BlockCypher:Endpoints")
            .GetChildren().Select(c => c["Name"]).ToList();
    }
    
    public Task<IEnumerable<BlockchainBase>> ListBlockchains(string name, DateTime? from, DateTime? to, int? limit, CancellationToken token)
    {
        return _repository.List(name, from, to, limit, token);
    }

    public bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) &&
               _endpointNames.Contains(name);
    }
}