namespace ThingsLoan.WebAPI.Entities
{
    public class Loan : BaseEntity
    {
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Person Person { get; set; }
        public bool Returned { get; set; }
    }
}
