using Domain.Models;
using Infrastructure.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace Infrastructure.Extensions
{
    internal static class BlockchainExtensions
    {
        internal static BlockchainDocument ToBlockchainDocument(this Blockchain blockchain)
        {
            return new BlockchainDocument()
            {
                CreatedAt = blockchain.CreatedAt,
                Metadata = new () { Name = blockchain.Name },
                Data =blockchain.Data
            };
        }
    }
}
