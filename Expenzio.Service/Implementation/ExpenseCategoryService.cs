using System.Net;
using System.Security.Claims;
using AutoMapper;
using expenzio.DAL.Interfaces;
using Expenzio.Common.Exceptions;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests;
using Expenzio.Domain.Models.Responses;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Expenzio.Service.Implementation;

/// <inheritdoc />
public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly IExpenseCategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;

    public ExpenseCategoryService(
        IExpenseCategoryRepository categoryRespository,
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    )
    {
        _categoryRepository = categoryRespository;
        _userRepository = userRepository;
        _httpContext = httpContextAccessor.HttpContext;
        _mapper = mapper;
    }

    // TODO: Unit test
    /// <inheritdoc />
    public async  Task<ApiResponse> CreateExpenseCategoryAsync(CreateExpenseCategoryRequest request, CancellationToken cancellationToken)
    {
        var userId = await GetUserIdFromRequest();
        var category = _mapper.Map<ExpenseCategory>(request);
        category.UserId = userId;
        await _categoryRepository.AddAsync(category, cancellationToken);
        return new ApiResponse
        (
            success: true,
            statusCode: (int)HttpStatusCode.Created,
            message: "Category created successfully"
        );
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExpenseCategory>> GetCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    /// <summary>
    /// Verifies the user based on the <see cref="ClaimTypes.NameIdentifier" /> claim.
    /// </summary>
    /// <exception cref="UnauthorizedException">Thrown if the user is not authenticated.</exception>
    /// <exception cref="NotFoundException">Thrown if the user is not found.</exception>
    /// <returns>The user id.</returns>
    private async Task<Guid> GetUserIdFromRequest()
    {
        var nameIdentifierClaim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        UnauthorizedException.ThrowIfNullOrEmpty(nameIdentifierClaim?.Value, "User is not authenticated");
        var userId = Guid.Parse(nameIdentifierClaim!.Value);
        var userExists = await _userRepository.ExistsAsync(u => !u.IsDeleted && u.Id == userId);
        NotFoundException.ThrowIfTrue(!userExists, "User not found");
        return userId;
    }
}
