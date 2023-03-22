﻿namespace Auth.Api.Domain;

public interface IUserIdentityRepository
{
    UserIdentity GetByUsername(string username);
}
