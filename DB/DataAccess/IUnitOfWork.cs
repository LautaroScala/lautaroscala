using DB.DataAccess.RepoInterfaces;

namespace DB.DataAccess
{
    public interface IUnitOfWork
    {
        IThingRepository ThingsRepository { get; }
        ILoanRepository LoanRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPersonRepository PersonRepository { get; }

        int Complete();
    }
}
