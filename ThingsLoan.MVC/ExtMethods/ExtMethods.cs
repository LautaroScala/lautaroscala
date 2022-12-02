using ThingsLoan.MVC.Models;
using ThingsLoan.WebAPI.Entities;

namespace ThingsLoan.MVC.ExtMethods
{
    public static class ABMExtMethods
    {
        public static ABMViewModel ToViewModel(this Things things)
        {
            return new ABMViewModel
            {
                Id = things.Id,
                Desc = things.Desc,
                CategoryId = things.CategoryId,
                CreationDate = things.CreationDate
            };
        }

        public static List<ABMViewModel> ToViewModels(this List<Things> things)
        {
            var list = new List<ABMViewModel>();
            things.ForEach(a => list.Add(a.ToViewModel()));

            return list;
        }

        public static Things ToEntity(this ABMViewModel things)
        {
            return new Things
            {
                Id = things.Id,
                Desc = things.Desc,
                CategoryId = things.CategoryId,
                CreationDate = DateTime.UtcNow
            };
        }
    }
}
