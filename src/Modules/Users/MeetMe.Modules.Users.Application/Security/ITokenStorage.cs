﻿using MeetMe.Modules.Users.Application.DTO;

namespace MeetMe.Modules.Users.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}