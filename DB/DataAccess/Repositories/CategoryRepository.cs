using DB.DataAccess.Context;
using DB.DataAccess.RepoInterfaces;
using DB.Entities;

namespace DB.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ThingsContext context):base(context) 
        {
        }

        public bool CheckDuplicate(string description)
        {
            return context.Categories.Any(x=> x.Desc == description);
        }
    }
}
