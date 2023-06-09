﻿namespace Auth.Api.Domain;

public class UserIdentity
{
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth {  get; set; }
    public string[] Roles { get; set; } = Array.Empty<string>();
    public string License { get; set; }
}
