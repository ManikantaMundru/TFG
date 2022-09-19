
using TFG.Assessment.Domain.Requests;
using TFG.Assessment.Domain.Response;

namespace TFG.Assessment.Api.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> Authenticate(AuthenticationRequest authRequest);
    }
}
