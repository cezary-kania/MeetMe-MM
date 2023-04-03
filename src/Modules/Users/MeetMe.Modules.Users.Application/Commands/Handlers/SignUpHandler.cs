using MeetMe.Modules.Users.Application.Exceptions;
using MeetMe.Modules.Users.Application.Security;
using MeetMe.Modules.Users.Domain.Entities;
using MeetMe.Modules.Users.Domain.Repositories;
using MeetMe.Modules.Users.Domain.valueTypes;
using MeetMe.Modules.Users.Shared.Events;
using MeetMe.Shared.Abstractions.Commands;
using MeetMe.Shared.Abstractions.Messaging;
using MeetMe.Shared.Abstractions.Services;

namespace MeetMe.Modules.Users.Application.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;

    public SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(SignUp command, CancellationToken cancellationToken = default)
    {
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var passwordHash = _passwordManager.Secure(command.Password);
        
        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }
        
        var user = new User(userId, email, passwordHash);
        await _userRepository.AddAsync(user);
        await _messageBroker.PublishAsync(new UserCreated(userId, email));
    }
}