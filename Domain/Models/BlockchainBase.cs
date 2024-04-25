namespace Domain.Models;

public class BlockchainBase
{
    public required DateTime CreatedAt { get; set; }
    public required BlockchainData Data { get; set; }
}