using DB.Entities;

namespace DB.DataAccess.RepoInterfaces
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        public Person GetPersonByUsername(string username);
    }
}
