using MeetMe.Modules.Users.Application.Exceptions;
using MeetMe.Modules.Users.Application.Security;
using MeetMe.Modules.Users.Domain.Repositories;
using MeetMe.Shared.Abstractions.Commands;

namespace MeetMe.Modules.Users.Application.Commands.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    

    public SignInHandler(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager, ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }

    public async Task HandleAsync(SignIn command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialsExceptions();
        }

        if (!_passwordManager.Validate(command.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsExceptions();
        }

        var jwt = _authenticator.CreateToken(user.Id);
        _tokenStorage.Set(jwt);
    }
}