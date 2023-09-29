using AccountService.API.Contracts;
using AccountService.API.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Features.Accounts;

[ApiController]
public class AccountController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IMediator mediator, ILogger<AccountController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [Route("api/account")]
    public async Task<IActionResult> Create(CreateAccountRequest request)
    {
        var applicationRequest = request.ToCommand();
        var response = await _mediator.Send(applicationRequest);

        return Ok(response);
    }
}