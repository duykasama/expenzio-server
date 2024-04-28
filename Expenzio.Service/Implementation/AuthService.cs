using System.Text.RegularExpressions;
using AutoMapper;
using Expenzio.Common.Exceptions;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Domain.Models.Responses;
using Expenzio.Service.Helpers;
using Expenzio.Service.Interfaces;

namespace Expenzio.Service.Implementation;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public Task<ApiResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<ExpenzioUser>(request);
        var (validUser, errorMessage) = ValidateUser(user);
        if (!validUser) throw new BadRequestException(errorMessage);
        var userExists = await _userRepository.ExistsAsync(u => u.Email == user.Email);
        if (userExists) throw new ConflictException("User with this email already exists");

        user.Password = PasswordHelper.HashPassword(user.Password);
        await _userRepository.AddAsync(user, cancellationToken);
        return new ApiResponse(true, 201, "User registered successfully");
    }

    private (bool isValid, string errorMessage) ValidateUser(ExpenzioUser user)
    {
        if (string.IsNullOrWhiteSpace(user.Email))
            return (false, "Email is required");
        if (string.IsNullOrWhiteSpace(user.Password))
            return (false, "Password is required");
        if (string.IsNullOrWhiteSpace(user.Phone))
            return (false, "Phone is required");
        if (string.IsNullOrWhiteSpace(user.FirstName))
            return (false, "First name is required");
        if (string.IsNullOrWhiteSpace(user.LastName))
            return (false, "Last name is required");
        if (!Regex.IsMatch(user.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            return (false, "Invalid email address");
        if (!Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
            return (false, "Password must contain at least 1 lowercase letter, 1 uppercase letter, 1 digit, 1 special character and must be at least 8 characters long");

        return (true, string.Empty);
    }
}
