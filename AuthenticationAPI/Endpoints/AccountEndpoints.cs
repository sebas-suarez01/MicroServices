using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Endpoints;

public static class AccountEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("auth", Authenticate)
            .WithName(nameof(Authenticate));
    }
    
    public static IResult Authenticate([FromBody]AuthenticationRequest request, JwtTokenHandler jwtTokenHandler)
    {
        var authenticationResponse = jwtTokenHandler.GenerateJwtToken(request);
        if (authenticationResponse is null)
            return Results.Unauthorized();
        
        return Results.Ok(authenticationResponse);
    }
}