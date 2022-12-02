using ThingsLoan.WebAPI.Entities;

namespace ThingsLoan.WebAPI.DataAccess.RepoInterfaces
{
    public interface IThingRepository : IGenericRepository<Things>
    {
        public Things GetThingsByDesc(string desc);
    }
}
