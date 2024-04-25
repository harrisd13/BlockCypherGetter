using Domain.Models;
using MediatR;

namespace Application.BlockchainQuery.Queries;

public record GetBlockchainsQuery : IRequest<IEnumerable<BlockchainBase>>
{
    public required string Name { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
    public int? Limit { get; init; }
}