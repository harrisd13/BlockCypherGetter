using Application.BlockchainQuery.Queries;
using Domain.Models;
using MediatR;

namespace Application.BlockchainQuery.Handlers;

public class GetBlockchainHandler : IRequestHandler<GetBlockchainsQuery, IEnumerable<BlockchainBase>>
{
    private readonly IBlockchainQueryService _blockchainQueryService;

    public GetBlockchainHandler(IBlockchainQueryService blockchainQueryService)
    {
        _blockchainQueryService = blockchainQueryService;
    }
    
    public Task<IEnumerable<BlockchainBase>> Handle(GetBlockchainsQuery request, CancellationToken cancellationToken)
    {
        return _blockchainQueryService.ListBlockchains(request.Name, request.From, request.To, request.Limit,
            cancellationToken);
    }
}