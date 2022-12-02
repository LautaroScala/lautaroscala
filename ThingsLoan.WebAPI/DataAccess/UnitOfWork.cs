using ThingsLoan.WebAPI.DataAccess.Context;
using ThingsLoan.WebAPI.DataAccess.RepoInterfaces;
using ThingsLoan.WebAPI.DataAccess.Repositories;

namespace ThingsLoan.WebAPI.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ThingsContext context;

        public IThingRepository ThingsRepository { get;private set; }

        public ILoanRepository LoanRepository { get;private set; }

        public ICategoryRepository CategoryRepository { get;private set; }

        public IPersonRepository PersonRepository { get;private set; }
        public UnitOfWork (ThingsContext context) 
        {
            this.context = context;
            ThingsRepository = new ThingRepository(context);
            LoanRepository = new LoanRepository(context);
            CategoryRepository = new CategoryRepository(context);
            PersonRepository = new PersonRepository(context);
        }
        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
