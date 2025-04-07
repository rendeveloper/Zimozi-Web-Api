using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZimoziSolutions.ApiModels.Tokens;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Core.Interfaces.Users;
using ZimoziSolutions.Domain.Users;
using ZimoziSolutions.Exceptions.Business;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Core.Users
{
    public class AuthCoreService : IAuthCoreService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthCoreService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenResponseModel> LoginAsync(UserModel request)
        {
            var user = await _userRepository.GetByNameAsync(request.Username);
            if(user is null)
                throw new BusinessSolutionException(HttpStatusCode.Conflict, ApplicationContext.Texts.GetValue(nameof(Texts.CustomValidation), Constants.InvalidUserAndPass));

            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                throw new BusinessSolutionException(HttpStatusCode.Conflict, ApplicationContext.Texts.GetValue(nameof(Texts.CustomValidation), Constants.InvalidUserAndPass));

            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseModel> CreateTokenResponse(User? user)
        {
            return new TokenResponseModel
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        public async Task<User?> RegisterAsync(UserModel request)
        {
            var username = await _userRepository.GetByNameAsync(request.Username);
            if (username != null)
                throw new BusinessSolutionException(HttpStatusCode.Conflict, ApplicationContext.Texts.GetValue(nameof(Texts.CustomValidation), Constants.AlreadyExistsUsername));

            var model = new User();
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(model, request.Password);

            model.Username = request.Username;
            model.PasswordHash = hashedPassword;

            User user = _mapper.Map<User>(model);
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return user;
        }

        public async Task<TokenResponseModel?> RefreshTokensAsync(RefreshTokenRequestModel request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return null;

            return await CreateTokenResponse(user);
        }

        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User model)
        {
            var refreshToken = GenerateRefreshToken();
            model.RefreshToken = refreshToken;
            model.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            User user = _mapper.Map<User>(model);
            _userRepository.Update(user);
            await _unitOfWork.SaveAsync();
            return refreshToken;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(ApplicationContext.AppSetting.Token!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: ApplicationContext.AppSetting.Issuer,
                audience: ApplicationContext.AppSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
