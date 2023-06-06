using DB.Entities;

namespace DB.DataAccess.RepoInterfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        public List<Loan> GetAllLoansById(int personId);
    }
}
