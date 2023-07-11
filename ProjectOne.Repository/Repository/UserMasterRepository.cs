using Newtonsoft.Json;
using ProjectOne.Data.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Repository.Repository
{
    public interface IUserMasterRepository
    {
        string getUserNameById(long Id);
    }
    public class UserMasterRepository : IUserMasterRepository
    {
        private readonly ProjectOneContext Context;
        public UserMasterRepository(ProjectOneContext context) { 
            this.Context = context;
        }
        public string getUserNameById(long Id)
        {

            var k = new MenuMaster();
            k.Id = 1;
            k.Name = "Dashboard";
            k.Code = "001";
            k.IsActive = 1;
            Context.SaveChanges();

            var getById = Context.UserMasters.Where(x => x.Id == Id).Select( x=>x.Name).FirstOrDefault() ?? "";
            return JsonConvert.SerializeObject(getById);

        }
    }
}
