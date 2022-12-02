namespace ThingsLoan.WebAPI.Entities
{
    public class Things : Entity 
    {
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
