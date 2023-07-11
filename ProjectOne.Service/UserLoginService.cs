using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectOne.Common;
using ProjectOne.Common.Helpers;
using ProjectOne.Data.Entitiy;
using ProjectOne.Models;
using ProjectOne.Repository.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Service
{
    public interface IUserLoginService
    {
        UserResult checklogin(UserLoginModel model);
    }

    public class UserLoginService : IUserLoginService
    {
         private readonly IMapper _mapper;
        private readonly ILoginRepository _loginRepository;
        private readonly AppSettings _appSettings;
        public UserLoginService(ILoginRepository loginRepository,IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
            this._appSettings = appSettings.Value;
        }

        public UserResult checklogin(UserLoginModel model)
        {
            var userLoginData = _loginRepository.checklogin(_mapper.Map<UserLoginEntity>(model)); 
            if (userLoginData != null)
            {
                if(userLoginData.LoginStatus == 1)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userLoginData.LoginName),
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    userLoginData.Token = tokenHandler.WriteToken(token);
                }

            }
            return userLoginData;
        }
    }
}
