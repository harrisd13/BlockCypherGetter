using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Extensions;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class BlockchainRepository : IBlockchainRepository
{
    private readonly IMongoCollection<BlockchainDocument> _collection;

    public BlockchainRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration.GetSection("DatabaseName").Value;
        var mongoDatabase = mongoClient.GetDatabase(databaseName);
        _collection = mongoDatabase.GetCollection<BlockchainDocument>("blockchain");
    }

    public async Task<IEnumerable<BlockchainBase>> List(string name, DateTime? from, DateTime? to, int? limit, CancellationToken token)
    {
        var filter = Builders<BlockchainDocument>.Filter.Eq(d => d.Metadata.Name, name);
        
        if (from != null)
        {
            filter &= Builders<BlockchainDocument>.Filter.Gte(d => d.CreatedAt, from);
        }

        if (to != null)
        {
            filter &= Builders<BlockchainDocument>.Filter.Lte(d => d.CreatedAt, to);
        }

        var sort = Builders<BlockchainDocument>.Sort.Descending(d => d.CreatedAt);

        var results = await _collection
            .Find(filter)
            .Sort(sort)
            .Limit(limit)
            .ToListAsync(cancellationToken: token);

        return results.Select(doc => new BlockchainBase()
        {
            CreatedAt = doc.CreatedAt,
            Data = doc.Data
        });
    }

    public async Task Add(Blockchain blockchain, CancellationToken token)
    {
        var blockchainDocument = blockchain.ToBlockchainDocument();
        await _collection.InsertOneAsync(blockchainDocument, cancellationToken: token);
    }

    public async Task BulkAdd(List<Blockchain> blockchains, CancellationToken token)
    {
        var blockchainDocuments = blockchains.Select(b => b.ToBlockchainDocument());
        await _collection.InsertManyAsync(blockchainDocuments, cancellationToken: token);
    }
}