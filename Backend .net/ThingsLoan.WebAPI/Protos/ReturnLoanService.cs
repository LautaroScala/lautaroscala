using DB.DataAccess;
using Grpc.Core;

namespace ThingsLoan.WebAPI.Protos
{
    public class ReturnLoanService : ReturnLoan.ReturnLoanBase
    {
        private readonly IUnitOfWork uow;
        public ReturnLoanService (IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public override Task<ReplyLoan> ReturnThing(ThingLoanDto request, ServerCallContext context)
        {
            var thingGet = uow.ThingsRepository.GetEntById(request.ThingId);
            var loanGet = uow.LoanRepository.GetEntById(request.LoanId);
            loanGet.Returned = true;
            loanGet.ReturnDate = DateTime.UtcNow;
            thingGet.Available = true;
            uow.LoanRepository.Update(loanGet);
            uow.ThingsRepository.Update(thingGet);
            uow.Complete();
            var mensaje =  new ReplyLoan { Message = $"{thingGet.Desc} fue devuelto." };
            return Task.FromResult(mensaje);
        }
    }
}
