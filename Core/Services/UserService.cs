using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System.Linq;

namespace Core.Services;

public class UserService : IUserService
{
    IBaseRepository<User> _userRepository;

    public UserService(IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetByEmail(string email)
    {
        return _userRepository.GetByPredicate(x => x.Email == email).FirstOrDefault();
    }

    public bool Add(User user)
    {
        return _userRepository.Add(user);
    }

    public bool Update(User user)
    {
        return _userRepository.Update(user);
    }

    public bool Delete(User user)
    {
        return _userRepository.Delete(user);
    }
   
}
