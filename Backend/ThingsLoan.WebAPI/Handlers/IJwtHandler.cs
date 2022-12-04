using DB.DTO;

namespace ThingsLoan.WebAPI.Handlers
{
    public interface IJwtHandler
    {
        string GenerateJWT(UserLoginDto user, IEnumerable<string> roles);
    }
}
