using ThingsLoan.WebAPI.DataAccess.RepoInterfaces;

namespace ThingsLoan.MVC.Controllers.UnitOfWork
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
