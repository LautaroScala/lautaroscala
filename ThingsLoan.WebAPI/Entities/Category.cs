namespace ThingsLoan.WebAPI.Entities
{
    public class Category : Entity
    {
        public IList<Things> Things { get; set; }
    }
}
