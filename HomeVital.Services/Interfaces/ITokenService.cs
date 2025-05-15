// ITokenService.cs
using System;
using HomeVital.Models.Dtos;

namespace HomeVital.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
    }
}