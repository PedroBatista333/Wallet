using System.ComponentModel.DataAnnotations;

namespace Wallet.Web.ViewModels
{
    public class IncomeViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string Description { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public int Category { get; set; }
    }
}
