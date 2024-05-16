using DTOs.RealtorDTOs;

namespace Services
{
    public interface IAuthenticationService
    {
        event Action<string?>? LoginChange;
        ValueTask<string> GetJwtAsync();
        Task<DateTime> LoginAsync(RealtorLoginDTO userLogin);
        Task LogoutAsync();
    }
}