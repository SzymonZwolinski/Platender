﻿using Platender.Application.DTO;
using Platender.Core.Helpers;
using Platender.Core.Models;
using Platender.Core.Services;
using System.Security.Cryptography;

namespace Platender.Application.Providers
{
	public class AuthProvider : IAuthProvider
	{
		private readonly IAuthService _authService;

		public AuthProvider(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<UserDto> RegisterUserAsync(string userName, string password)
		{
			PasswordHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

			var user = await _authService.CreateUserAsync(userName, passwordHash, passwordSalt);

			return MapUserToUserDto(user);
		}

		public Task<string> LoginUserAsync(string userName, string password)
		{
			return _authService.CheckLogin(userName, password);
		}

		private UserDto MapUserToUserDto(User user)
		{
			return new UserDto(user.Username, user.userStatus.ToString());
		}
	}
}
