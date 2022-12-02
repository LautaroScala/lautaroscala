using ThingsLoan.WebAPI.DataAccess.Context;
using ThingsLoan.WebAPI.DataAccess.RepoInterfaces;
using ThingsLoan.WebAPI.Entities;

namespace ThingsLoan.WebAPI.DataAccess.Repositories
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
