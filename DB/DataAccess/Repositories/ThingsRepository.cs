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

        public List<Things> FilterByDesc(string desc)
        {
            if (string.IsNullOrEmpty(desc))
                return context.Things.ToList();
            var loweredSearch = desc.ToLowerInvariant();
            var filteredResults = context.Things
                .Where(x => x.Desc.ToLowerInvariant().Contains(loweredSearch)).ToList();
            return filteredResults;
        }
    }
}
