using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Entities
{
    public class Loan : BaseEntity
    {
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int PersonId { get; set; }
        public int ThingId { get; set; }
        public bool Returned { get; set; }
    }
}
