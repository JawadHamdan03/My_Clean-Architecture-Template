using cla.Application.Features.Accounts.Reponses;
using MediatR;

namespace cla.Application.Features.Accounts.LoginCommand;


public sealed record LoginCommand(string Name , string password): IRequest<TokenResponse>;