using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Product.API.Data;
using Product.API.Interfaces.Repository;
using Product.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Product.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductContext _productContext;
        private readonly AppSettings _appSettings;

        public UserRepository(ProductContext productContext, IOptions<AppSettings> appsettings)
        {
            _productContext = productContext;
            _appSettings = appsettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _productContext.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            //user not found
            if (user == null)
            {
                return null;
            }

            //if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _productContext.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
                return true;

            return false;
        }

        public User Register(string username, string password)
        {
            User user = new User()
            {
                Username = username,
                Password = password,
                Role = "Admin"
            };

            _productContext.Users.Add(user);
            _productContext.SaveChanges();
            user.Password = "";
            return user;
        }
    }
}
