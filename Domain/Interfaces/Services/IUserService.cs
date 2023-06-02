using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IUserService
{
    bool Add(User user);
    User GetByEmail(string email);
    bool Update(User user);
    bool Delete(User user);
}
