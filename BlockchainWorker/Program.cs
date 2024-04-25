using Application;
using Application.Abstractions;
using Application.BlockCypherApi;
using Domain.Interfaces;
using Infrastructure.Repositories;
using MongoDB.Driver;

namespace BlockchainWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<BlockchainWorker>();
            builder.Services.AddTransient<IBlockchainRepository, BlockchainRepository>();
            builder.Services.AddSingleton<IMongoClient>(s =>
                new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
            builder.Services.AddSingleton<IBlockchainStoreService, BlockchainStoreService>();
            builder.Services.AddSingleton<IBlockCypherApi, BlockCypherApi>();

            var host = builder.Build();
            host.Run();
        }
    }
}