using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoanApplicationSite.Models
{
    public class PlanSelectionDetails
    {
        [Required]
        [DisplayName("File Name One")]
        public string FileNameOne { get; set; }

        [Required]
        [DisplayName("File Name Two")]
        public string FileNameTwo { get; set; }
    }
}