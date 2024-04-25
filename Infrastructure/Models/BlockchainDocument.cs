using Domain.Models;

namespace Infrastructure.Models;

internal class BlockchainDocument
{
    public required DateTime CreatedAt { get; set; }

    public required BlockchainMetadata Metadata { get; set; }

    public required BlockchainData Data { get; set; }
}