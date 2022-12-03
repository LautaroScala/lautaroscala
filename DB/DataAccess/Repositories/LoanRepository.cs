using DB.DataAccess.Context;
using DB.DataAccess.RepoInterfaces;
using DB.Entities;

namespace DB.DataAccess.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(ThingsContext context) : base(context)
        {

        }

        public List<Loan> GetAllLoansById(int personId)
        {
            var allLoans = context.Loans.Where(x => x.PersonId == personId).ToList();

            return allLoans;
        }
    }
}
