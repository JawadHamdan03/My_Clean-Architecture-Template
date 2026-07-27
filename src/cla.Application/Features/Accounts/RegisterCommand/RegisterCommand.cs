using cla.Application.Features.Accounts.Reponses;
using MediatR;

namespace cla.Application.Features.Accounts.RegisterCommand;



public sealed record RegisterCommand(string Name , string Password):IRequest<TokenResponse>;