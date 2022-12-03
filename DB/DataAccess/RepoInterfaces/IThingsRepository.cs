using DB.Entities;

namespace DB.DataAccess.RepoInterfaces
{
    public interface IThingRepository : IGenericRepository<Things>
    {
        public Things GetThingsByDesc(string desc);
        public List<Things> FilterByDesc(string desc);
    }
}
