using DB.DataAccess;
using DB.DTO;
using DB.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Cryptography.X509Certificates;
using ThingsLoan.WebAPI.Controllers;

namespace ThingsLoan.UnitTests
{
    [TestClass]
    public class LoanControllerTests
    {
        private Mock<IUnitOfWork> uow;
        private LoanController target;

        [TestInitialize]
        public void Init()
        {
            uow= new Mock<IUnitOfWork>();
            target = new LoanController(uow.Object);
            uow.Setup(x => x.LoanRepository.GetEntById(0)).Returns(new Loan ());
        }
        [TestMethod]
        public void GetAllLoans_Returns_OK()
        {
            var result = target.GetAllLoans();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
        [TestMethod]
        public void GetPersonLoans_Returns_OK()
        {   
            var testLoan = new List<Loan>();
            testLoan.Add(new Loan() { Id = 1 });
            uow.Setup(x => x.LoanRepository.GetAllLoansById(1)).Returns(testLoan);

            var result = target.GetPersonLoans(1);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
        [TestMethod]
        public void GetPersonLoans_Returns_BadRequest_If_ID_is_0()
        {
            var result = target.GetPersonLoans(0);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        [TestMethod]
        public void GetPersonLoans_Returns_NotFound_when_no_loans()
        {
            uow.Setup(x => x.LoanRepository.GetAllLoansById(6)).Returns(new List<Loan>());
            var result = target.GetPersonLoans(6);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void CreateLoan_Returns_Created()
        {
            var loan = new LoanDto
            {
                PersonId = 0,
                ThingId = 0,
            };
            var mockperson = new Person();
            uow.Setup(x => x.PersonRepository.GetEntById(loan.PersonId)).Returns(mockperson);
            uow.Setup(x => x.ThingsRepository.GetEntById(loan.ThingId)).Returns(new Things { Available = true });
            uow.Setup(x => x.LoanRepository.Add(It.IsAny<Loan>())).Returns(new Loan());

            var result = target.MakeLoan(loan);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
        }
        [TestMethod]
        public void CreateLoan_Returns_BadRequest_When_Null()
        {
            var result = target.MakeLoan(null);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        [TestMethod]
        public void CreateLoan_Returns_NotFound_When_Person_Non_Existant()
        {
            var loanTest = new LoanDto
            {
                PersonId = 0,
                ThingId = 0
            };
            uow.Setup(x => x.PersonRepository.GetEntById(loanTest.PersonId)).Returns((Person)null);
            var result = target.MakeLoan(loanTest);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void ReturnThing_Returns_Ok()
        {
            var loanTest = new LoanDto
            {
                PersonId = 0,
                ThingId = 0
            };
            var ThingReturn = new Things();
            var LoanReturn = new Loan();
            uow.Setup(x => x.LoanRepository.Update(It.IsAny<Loan>())).Returns(LoanReturn);
            uow.Setup(x => x.ThingsRepository.Update(It.IsAny<Things>())).Returns(ThingReturn);
            uow.Setup(x => x.ThingsRepository.GetEntById(loanTest.ThingId)).Returns(ThingReturn);
            uow.Setup(x => x.PersonRepository.GetEntById(loanTest.PersonId)).Returns(new Person());
            uow.Setup(x => x.LoanRepository.GetEntById(0)).Returns(LoanReturn);

            var result = target.ReturnThing(loanTest,0);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
        [TestMethod]
        public void ReturnThing_Returns_BadRequest_if_null()
        {
            var result = target.ReturnThing(null,0);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        [TestMethod]
        public void ReturnThing_Returns_NotFound_if_thing_non_existant()
        {
            var loanTest = new LoanDto
            {
                PersonId = 0,
                ThingId = 0
            };
            uow.Setup(x => x.ThingsRepository.GetEntById(loanTest.ThingId)).Returns((Things)null);
            var result = target.ReturnThing(loanTest, 0);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void ReturnThing_Returns_BadRequest_if_thing_available()
        {
            var loanTest = new LoanDto
            {
                PersonId = 0,
                ThingId = 0
            };
            uow.Setup(x => x.ThingsRepository.GetEntById(loanTest.ThingId)).Returns(new Things { Available = true});
            var result = target.ReturnThing(loanTest, 0);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        [TestMethod]
        public void ReturnThing_Returns_NotFound_if_loan_isnull()
        {
            var loanTest = new LoanDto
            {
                PersonId = 0,
                ThingId = 0
            };
            uow.Setup(x => x.ThingsRepository.GetEntById(loanTest.ThingId)).Returns(new Things { Available = true });
            uow.Setup(x => x.LoanRepository.GetEntById(0)).Returns((Loan)null);
            var result = target.ReturnThing(loanTest, 0);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        [TestMethod]
        public void ReturnThing_Returns_NotFound_if_person_non_existant()
        {
            var loanTest = new LoanDto
            {
                PersonId = 0,
                ThingId = 0
            };
            uow.Setup(x => x.ThingsRepository.GetEntById(loanTest.ThingId)).Returns(new Things());
            uow.Setup(x => x.PersonRepository.GetEntById(loanTest.PersonId)).Returns((Person)null);
            var result = target.ReturnThing(loanTest, 0);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
