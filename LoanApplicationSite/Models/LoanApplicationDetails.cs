using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoanApplicationSite.Models
{
    public class LoanApplicationDetails
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Second Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Existing Account")]
        public AccountType ExistingAccount { get; set; }

        [Required]
        [DisplayName("I accept the terms and conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions")]
        public bool TermsAcceptance { get; set; } 
    }
}