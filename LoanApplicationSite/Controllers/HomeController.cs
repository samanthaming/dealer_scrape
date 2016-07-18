using System.Web.Mvc;
using LoanApplicationSite.Models;

namespace LoanApplicationSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StartLoanApplication()
        {            
            return View(new LoanApplicationDetails {ExistingAccount=AccountType.None});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitApplication(LoanApplicationDetails applicationDetails)
        {
            if (!ModelState.IsValid)
            {
                return View("StartLoanApplication", applicationDetails);
            }

            SimulateSavingToDatabaseOrBackendSystem();
            
            return View("ApplicationComplete", new ApplicationCompleteConfirmation {FirstName= applicationDetails.FirstName});
        }

        private void SimulateSavingToDatabaseOrBackendSystem()
        {
            // ...
        }

        public ActionResult ApplicationComplete(ApplicationCompleteConfirmation confirmationDetails)
        {
            return View(confirmationDetails);
        }
    }
}