using DB.DataAccess.Context;
using DB.DataAccess.RepoInterfaces;
using DB.Entities;

namespace DB.DataAccess.Repositories
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
