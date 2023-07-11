using ProjectOne.Repository;
namespace ProjectOne.Service
{
    public interface IClass1Service
    {
        string NameData();
    }
    public class Class1Service : IClass1Service
    {
        private readonly IClass1Repository _class1Repository;
        public Class1Service(IClass1Repository class1Repository) { 
        this._class1Repository = class1Repository;
        }
        public string NameData()
        {
            return this._class1Repository.Name();
        }
    }
}