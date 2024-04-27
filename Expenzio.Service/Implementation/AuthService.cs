using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Domain.Models.Responses;
using Expenzio.Service.Interfaces;

namespace Expenzio.Service.Implementation;

public class AuthService : IAuthService
{
    public Task<ApiResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
