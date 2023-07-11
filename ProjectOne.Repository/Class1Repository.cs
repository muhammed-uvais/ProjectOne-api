using ProjectOne.Data.DbEntities;

namespace ProjectOne.Repository
{
    public interface IClass1Repository
    {
        string Name();
    }
    public class Class1Repository : IClass1Repository
    {
        private readonly ProjectOneContext Context;
        public Class1Repository(ProjectOneContext context) { 
            this.Context = context;
        }
       public string Name() {

            var data = Context.UserLogins.FirstOrDefault(x => x.IsAdmin == 1);

                return data.Username??"";
        }
    }
}