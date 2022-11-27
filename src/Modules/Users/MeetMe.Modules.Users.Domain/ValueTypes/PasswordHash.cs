using MeetMe.Modules.Users.Domain.Exceptions;

namespace MeetMe.Modules.Users.Domain.valueTypes;

public sealed class PasswordHash
{
    public string Value { get; }
        
    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPasswordHashException(value);
        }
            
        Value = value;
    }

    public static implicit operator PasswordHash(string value) => value is null ? null : new PasswordHash(value);

    public static implicit operator string(PasswordHash value) => value?.Value;

    public override string ToString() => Value;
}