using Application.BlockchainQuery;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests;

public class BlockchainQueryServiceTests
{
    
    [Fact]
    public void IsValidName_ReturnsTrue_IfExists()
    {
        const string validName = "valid";
        var repositoryMock = new Mock<IBlockchainRepository>();
        var inMemorySettings = new Dictionary<string, string?> {
            ["BlockCypher:Endpoints:0:Name"] = validName
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        var blockchainQueryService = new BlockchainQueryService(repositoryMock.Object, configuration);
        Assert.True(blockchainQueryService.IsValidName(validName));
    }
    
    [Fact]
    public void IsValidName_ReturnsFalse_IfNotExists()
    {
        const string validName = "valid";
        var repositoryMock = new Mock<IBlockchainRepository>();
        var inMemorySettings = new Dictionary<string, string?> {
            ["BlockCypher:Endpoints:0:Name"] = validName
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        var blockchainQueryService = new BlockchainQueryService(repositoryMock.Object, configuration);
        Assert.False(blockchainQueryService.IsValidName("invalid"));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void IsValidName_ReturnsFalse_IfEmptyOrWhitespace(string invalidName)
    {
        const string validName = "valid";
        var repositoryMock = new Mock<IBlockchainRepository>();
        var inMemorySettings = new Dictionary<string, string?> {
            ["BlockCypher:Endpoints:0:Name"] = validName
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        var blockchainQueryService = new BlockchainQueryService(repositoryMock.Object, configuration);
        Assert.False(blockchainQueryService.IsValidName(invalidName));
    }
}