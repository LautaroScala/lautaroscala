using ThingsLoan.API.Models;
using ThingsLoan.WebAPI.Entities;

namespace ThingsLoan.API.Classes
{
    public static class ExtMethods
    {
        public static ThingsViewModel ToViewModel(this Things things)
        {
            return new ThingsViewModel
            {
                Id = things.Id,
                Category = things.Category,
                Desc = things.Desc
            };
        }
        public static List<ThingsViewModel> ToViewModels(this List<Things> things)
        {
            var list = new List<ThingsViewModel>();
            things.ForEach(x => list.Add(x.ToViewModel()));
            return list;
        }

        public static Things ToEntity(this Things things)
        {
            return new Things
            {
                Id = things.Id,
                Category = things.Category,
                Desc = things.Desc
            };
        }
    }
}
