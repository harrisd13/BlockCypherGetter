using MediatR;

namespace Application.BlockchainQuery.Queries;

public record BlockchainNameValidate : IRequest<bool>
{
    public required string Name { get; init; }
}