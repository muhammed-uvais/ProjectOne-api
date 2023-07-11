using ProjectOne.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Service
{
    public interface IUserMasterService{
        string getUserNameById(long Id);
    }
    public class UserMasterService : IUserMasterService
    {
        private readonly IUserMasterRepository _userMasterRepository;
        public UserMasterService(IUserMasterRepository userMasterRepository)
        {
            this._userMasterRepository = userMasterRepository;
        }
        public string getUserNameById(long Id)
        {
            return _userMasterRepository.getUserNameById(Id);
        }
    }
}
