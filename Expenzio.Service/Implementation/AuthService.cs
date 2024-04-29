using System.Text.RegularExpressions;
using AutoMapper;
using Expenzio.Common.Exceptions;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Domain.Models.Responses;
using Expenzio.Service.Extensions;
using Expenzio.Service.Helpers;
using Expenzio.Service.Interfaces;

namespace Expenzio.Service.Implementation;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public AuthService(
        IUserRepository userRepository,
        IMapper mapper,
        IJwtService jwtService
    )
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _mapper = mapper;
    }
    
    public async Task<ApiResponse<TokenResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var (validRequest, errorMessage) = ValidateLoginRequest(request);
        if (!validRequest) throw new BadRequestException(errorMessage);
        var user = await _userRepository.GetAsync(u => u.Email == request.Email, cancellationToken);
        if (user == null) throw new NotFoundException("User not found");
        if (!PasswordHelper.VerifyPassword(request.Password, user.Password)) throw new UnauthorizedException("Invalid password");
        return new ApiResponse<TokenResponse>
        (
            success: true,
            statusCode: 200,
            message: "Login successful",
            data: new ()
            {
                // TODO: Get roles from user
                AccessToken = _jwtService.GenerateAccessToken(user, new string[] { "User" }),
                RefreshToken = _jwtService.GenerateRefreshToken(user.Id),
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            }
        );
    }

    public async Task<ApiResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var (validRequest, errorMessage) = ValidateRegisterRequest(request);
        if (!validRequest) throw new BadRequestException(errorMessage);
        var user = _mapper.Map<ExpenzioUser>(request);
        var userExists = await _userRepository.ExistsAsync(u => u.Email == user.Email);
        if (userExists) throw new ConflictException("User with this email already exists");

        user.Password = PasswordHelper.HashPassword(user.Password);
        user.SetCreationInfo();
        await _userRepository.AddAsync(user, cancellationToken);
        return new ApiResponse(true, 201, "User registered successfully");
    }

    private (bool isValid, string errorMessage) ValidateRegisterRequest(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            return (false, "Email is required");
        if (string.IsNullOrWhiteSpace(request.Password))
            return (false, "Password is required");
        if (string.IsNullOrWhiteSpace(request.Username))
            return (false, "Username is required");
        if (string.IsNullOrWhiteSpace(request.Phone))
            return (false, "Phone is required");
        if (string.IsNullOrWhiteSpace(request.FirstName))
            return (false, "First name is required");
        if (string.IsNullOrWhiteSpace(request.LastName))
            return (false, "Last name is required");
        if (!Regex.IsMatch(request.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            return (false, "Invalid email address");
        if (!Regex.IsMatch(request.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
            return (false, "Password must contain at least 1 lowercase letter, 1 uppercase letter, 1 digit, 1 special character and must be at least 8 characters long");

        return (true, string.Empty);
    }

    private (bool isValid, string errorMessage) ValidateLoginRequest(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            return (false, "Email is required");
        if (string.IsNullOrWhiteSpace(request.Password))
            return (false, "Password is required");

        return (true, string.Empty);
    }
}
