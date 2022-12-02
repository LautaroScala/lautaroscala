using ThingsLoan.WebAPI.DataAccess.Context;
using ThingsLoan.WebAPI.DataAccess.RepoInterfaces;
using ThingsLoan.WebAPI.Entities;

namespace ThingsLoan.WebAPI.DataAccess.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(ThingsContext context) : base(context)
        {

        }

        public Person GetPersonByUsername(string username)
        {
            return context.Persons.FirstOrDefault(x => x.Username == username);
        }
    }
}
