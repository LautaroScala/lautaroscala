using DB.DataAccess.Context;
using DB.DataAccess.RepoInterfaces;
using DB.Entities;

namespace DB.DataAccess.Repositories
{
    public class ThingRepository : GenericRepository<Things>, IThingRepository
    {
        public ThingRepository(ThingsContext context) : base(context)
        {

        }

        public Things GetThingsByDesc(string desc)
        {
            return context.Things.FirstOrDefault(x => x.Desc == desc);
        }
    }
}
