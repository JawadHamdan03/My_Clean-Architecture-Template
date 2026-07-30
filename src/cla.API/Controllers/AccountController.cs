using cla.API.Requests;
using cla.Application.Features.Accounts.LoginCommand;
using cla.Application.Features.Accounts.RefreshTokenCommand;
using cla.Application.Features.Accounts.RegisterCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cla.API.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> login(LoginRequest loginRequest)
    {
        var res= await mediator.Send(new LoginCommand(loginRequest.Name,loginRequest.Password));
        return Ok(res);
    }


    [HttpPost("register")]
    public async Task<IActionResult> registerUser(RegisterRequest registerRequest)
    {
        var res =await mediator.Send(new RegisterCommand(registerRequest.Name,registerRequest.Password));
        return Ok(res);
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> refreshToken([FromBody]string refreshToken)
    {
        var res = await mediator.Send(new RefreshTokenCommand(refreshToken));
        return Ok(res);
    }

}