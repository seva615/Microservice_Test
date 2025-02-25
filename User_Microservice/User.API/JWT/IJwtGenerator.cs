using User.API.ControllerModels;

namespace User.API.JWT
{
    public interface IJwtGenerator
    {
        string GenerateJwtToken(UserViewModel user);
    }
}
