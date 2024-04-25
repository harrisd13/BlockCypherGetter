using System.Text.Json;
using Application.Abstractions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.BlockCypherApi;

public class BlockCypherApi : IBlockCypherApi
{
    private readonly HttpClient _client;
    private readonly ILogger<BlockCypherApi> _logger;
    private readonly string _baseUrl;

    public BlockCypherApi(IConfiguration configuration, ILogger<BlockCypherApi> logger)
    {
        _baseUrl = configuration.GetSection("BlockCypher:BaseUrl").Value
                   ?? throw new InvalidOperationException("BlockCypher's base url configuration is missing");
        _client = new HttpClient();
        _logger = logger;
    }
    
    public async Task<Blockchain?> GetBlockChain(BlockchainEndpoint endpoint, CancellationToken token)
    {
        HttpResponseMessage response;
        var uri = _baseUrl + endpoint.Endpoint;

        try
        {
            response = await _client.GetAsync(uri, token);
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to get url: {uri}.", e);
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to get url: {uri}. Status code: {response.StatusCode}");
            return null;
        }

        var content = await response.Content.ReadAsStringAsync(token);
        if (string.IsNullOrWhiteSpace(content))
        {
            _logger.LogError($"Content is empty. Url: {uri}");
            return null;
        }

        var data = JsonSerializer.Deserialize<BlockchainData>(content, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        })!;

        return new Blockchain()
        {
            CreatedAt = DateTime.UtcNow,
            Name = endpoint.Name,
            Data = data
        };
    }
}