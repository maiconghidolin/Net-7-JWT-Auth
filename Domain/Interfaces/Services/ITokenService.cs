using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface ITokenService
{
    string CreateToken(User user);
}
