using System;
using System.Web.Mvc;
using System.Web.Services.Description;
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

        //public ActionResult SubmitForm(PlanSelectionDetails planSelectionDetails)
        //{

        //    return View("ComparePlansResult", new ComparePlansResultConfirmation {FilesName = planSelectionDetails});
        //}

        public ActionResult SubmitForm()
        {
            string[] fileList = Request.Form.GetValues("vehicle");
            string[] provinceList = Request.Form.GetValues("province");


            if (fileList != null && provinceList != null)
            {
                if (fileList.Length == 2 && provinceList.Length == 1)
                {
                    ViewData["selectedFileOne"] = fileList[0];
                    ViewData["selectedFileTwo"] = fileList[1];
                    ViewData["selectedProvince"] = provinceList[0];

                    return View("ComparePlansResult");
                }

                ViewData["CheckBoxError"] = "Please select 2 files only";
                return View("Index");
            }

            ViewData["CheckBoxError"] = "Please select 2 files to compare";
            return View("Index");
        }
    }
}