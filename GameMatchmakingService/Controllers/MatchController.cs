using System.Net;
using GameMatchmakingService.Models;
using GameMatchmakingService.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameMatchmakingService.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public MatchController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    
    [HttpPost, Route("Enqueue")]
    public async Task<IActionResult> Enqueue([FromBody] PlayerInfo playerInfo)
    {
        using var reader = new StreamReader(HttpContext.Request.Body);
        var memory = new Memory<char>(new char[1024]); 
        
        await reader.ReadAsync(memory);
        
        var body = memory.ToString();
        //var playerInfo = JsonConvert.DeserializeObject<PlayerInfo>(body);

        if (playerInfo == null)
            return StatusCode(400);
        
        var authorized = await _authorizationService.AuthorizeAsync(playerInfo);
        
        return authorized ? Ok() : Unauthorized();
    }
}