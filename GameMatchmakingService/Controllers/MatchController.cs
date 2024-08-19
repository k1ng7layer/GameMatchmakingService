using System.Net;
using GameMatchmakingService.Models;
using GameMatchmakingService.Services.Authorization;
using GameMatchmakingService.Services.GameQueue;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameMatchmakingService.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IGameQueueService _gameQueueService;

    public MatchController(
        IAuthorizationService authorizationService, 
        IGameQueueService gameQueueService
    )
    {
        _authorizationService = authorizationService;
        _gameQueueService = gameQueueService;
    }
    
    [HttpPost, Route("enqueue")]
    public async Task<IActionResult> Enqueue([FromBody] PlayerInfo playerInfo)
    {
        using var reader = new StreamReader(HttpContext.Request.Body);
        var memory = new Memory<char>(new char[1024]); 
        
        await reader.ReadAsync(memory);
        
        if (playerInfo == null)
            return StatusCode(400);
        
        var authorized = await _authorizationService.AuthorizeAsync(playerInfo);
        
        if (!authorized)
            return Unauthorized();
        
        _gameQueueService.Enqueue(playerInfo.Login);

        return Ok();
    }
}