using ProjectOne.Common;
using ProjectOne.Data.DbEntities;
using ProjectOne.Data.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Repository.Repository
{
    public interface ILoginRepository
    {
        UserResult checklogin(UserLoginEntity entity);
    }
    public class LoginRepository : ILoginRepository 
    {
        private readonly ProjectOneContext Context;
        private readonly UserResult result;
        public LoginRepository(ProjectOneContext context, UserResult result)
        {
            this.Context = context;
            this.result = result;
        }
        public UserResult checklogin(UserLoginEntity entity)
        {
           List<string>Error = new List<string>();
            
            var userData = (from login in Context.UserLogins
                            where login.Username == entity.Username
                            && login.Password == entity.Password
                            && login.IsActive == 1
                            select new UserResult()
                            {
                                LoginName = login.Username,
                                LoginId = login.Id,
                                LoginStatus = 1,
                                IsAdmin = login.IsAdmin,
                                RoleName = "Admin",



                            }).FirstOrDefault();
            if(userData != null )
            {
                userData.ErrorCodes = Error;
                return userData;
            }
            else
            {
                var st = "User Not Valid";
                Error.Add(st);
                
            }
            result.ErrorCodes = Error;
            result.LoginStatus = 0;
            return result;
        }
    }
}
