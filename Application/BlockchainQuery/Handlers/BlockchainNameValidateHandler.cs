using Application.BlockchainQuery.Queries;
using MediatR;

namespace Application.BlockchainQuery.Handlers;

public class BlockchainNameValidateHandler : IRequestHandler<BlockchainNameValidate, bool>
{
    private readonly IBlockchainQueryService _blockchainQueryService;

    public BlockchainNameValidateHandler(IBlockchainQueryService blockchainQueryService)
    {
        _blockchainQueryService = blockchainQueryService;
    }
    
    public Task<bool> Handle(BlockchainNameValidate request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_blockchainQueryService.IsValidName(request.Name));
    }
}