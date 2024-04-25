using System.Collections.Concurrent;
using Application.Abstractions;
using Application.BlockCypherApi;
using Domain.Models;

namespace BlockchainWorker
{
    public class BlockchainWorker : BackgroundService
    {
        private const int MaxRequestsPerHour = 100;
        private readonly IServiceProvider _provider;
        private readonly ILogger<BlockchainWorker> _logger;
        private readonly List<BlockchainEndpoint> _endpoints;
        private readonly ConcurrentBag<Blockchain> _failedSaves;
        private readonly ConcurrentDictionary<string, BlockchainData> _lastResponse;

        public BlockchainWorker(IServiceProvider provider, ILogger<BlockchainWorker> logger,
            IConfiguration configuration)
        {
            _provider = provider;
            _logger = logger;
            
            var endpoints = configuration.GetSection("BlockCypher:Endpoints").Get<List<BlockchainEndpoint>>();
            if (endpoints == null || !endpoints.Any())
            {
                throw new InvalidOperationException("BlockCypher endpoint configuration is missing");
            }

            _endpoints = endpoints;
            _failedSaves = new ConcurrentBag<Blockchain>();
            _lastResponse = new ConcurrentDictionary<string, BlockchainData>();

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Executing {nameof(BlockchainWorker)}. Endpoints: {_endpoints.Count}");
            await using var scope = _provider.CreateAsyncScope();
            var blockChainStorage = scope.ServiceProvider.GetRequiredService<IBlockchainStoreService>();
            var blockCypherApi = scope.ServiceProvider.GetRequiredService<IBlockCypherApi>();
            var delayInMinutes = 60 / ((decimal)MaxRequestsPerHour / _endpoints.Count);
            var delayInMs = (int)Math.Floor(delayInMinutes * 60 * 1000);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    foreach (var endpointsBatch in _endpoints.Chunk(3))
                    {
                        await Parallel.ForEachAsync(endpointsBatch, stoppingToken, async (endpoint, token) =>
                        {
                            var blockchain = await blockCypherApi.GetBlockChain(endpoint, token);
                            if (blockchain == null)
                            {
                                return;
                            }

                            if (_lastResponse.TryGetValue(endpoint.Name, out var previousContent) &&
                                blockchain.Data.Equals(previousContent))
                            {
                                return;
                            }

                            _lastResponse[endpoint.Name] = blockchain.Data;

                            if (!await blockChainStorage.SaveBlockchain(blockchain, token))
                            {
                                _failedSaves.Add(blockchain);
                            }
                        });

                        if (_failedSaves.Count > 0)
                        {
                            var blockChains = _failedSaves.ToList();
                            if (await blockChainStorage.SaveBlockchains(blockChains, stoppingToken))
                            {
                                _failedSaves.Clear();
                            }
                        }

                        await Task.Delay(2000, stoppingToken);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while getting blockchain data");
                }

                await Task.Delay(delayInMs, stoppingToken);
            }
        }
    }
}
