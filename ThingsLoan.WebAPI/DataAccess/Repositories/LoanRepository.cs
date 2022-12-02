using ThingsLoan.WebAPI.DataAccess.Context;
using ThingsLoan.WebAPI.DataAccess.RepoInterfaces;
using ThingsLoan.WebAPI.Entities;

namespace ThingsLoan.WebAPI.DataAccess.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(ThingsContext context) : base(context)
        {

        }
    }
}
