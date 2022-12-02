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
    }
}
