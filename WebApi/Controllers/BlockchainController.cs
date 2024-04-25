using System.ComponentModel.DataAnnotations;
using Application.BlockchainQuery;
using Application.BlockchainQuery.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

/// <summary>
/// Blockchain access controller
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class BlockchainController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BlockchainController> _logger;

    /// <summary>
    /// Blockchain controller. Uses query service to access blockchain data
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public BlockchainController(ILogger<BlockchainController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a list of blockchain data for the specified name
    /// </summary>
    /// <param name="name">Name of blockchain</param>
    /// <param name="from">From date</param>
    /// <param name="to">To date</param>
    /// <param name="limit">Limit results</param>
    /// <param name="token"></param>
    /// <returns>A list of blockchain data ordered by the most recent first</returns>
    /// <response code="200">Returns the list of blockchain data</response>
    /// <response code="404">If the name is not found</response>
    [HttpGet]
    [Route("{name}", Name = "GetBlockchains")]
    [ProducesResponseType(typeof(List<BlockchainBase>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([Required] string name, DateTime? from, DateTime? to, int? limit, CancellationToken token)
    {
        if (!await _mediator.Send(new BlockchainNameValidate()
            {
                Name = name
            }))
        {
            _logger.LogWarning($"Invalid blockchain name was requested: {name}");
            return NotFound();
        }

        var result = await _mediator.Send(new GetBlockchainsQuery()
        {
            Name = name,
            From = from,
            To = to,
            Limit = limit
        }, token);

        return Ok(result);
    }
}