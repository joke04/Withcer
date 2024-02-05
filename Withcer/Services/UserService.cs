using Microsoft.EntityFrameworkCore;
using Withcer.Models;
using System.Linq;

namespace Withcer.Services
{
    public interface IUserService
    {
        User Authenticate (string Email, string Password);
    }
    public class UserService : IUserService
    {
       
        private readonly Data _dataLink;

        public UserService(Data dataLink)
        {
            _dataLink = dataLink;
        }
        public User Authenticate(string email, string password)
        {
            var user = _dataLink.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
                return null;

            // Аутентификация успешна
            return user;
        }

    }
}
