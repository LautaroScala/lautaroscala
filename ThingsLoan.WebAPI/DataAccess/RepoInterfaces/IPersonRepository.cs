using ThingsLoan.WebAPI.Entities;

namespace ThingsLoan.WebAPI.DataAccess.RepoInterfaces
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        public Person GetPersonByUsername(string username);
    }
}
