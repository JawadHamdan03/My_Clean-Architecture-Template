using cla.API.Requests;
using cla.Application.Features.Accounts.LoginCommand;
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

}