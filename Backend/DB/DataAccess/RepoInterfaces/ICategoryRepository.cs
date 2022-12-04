using DB.Entities;
    
namespace DB.DataAccess.RepoInterfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public bool CheckDuplicate(string description);
        public Category GetByDescription(string description);
    }
}
