using System.ComponentModel.DataAnnotations;

namespace Wallet.Web.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string Description { get; set; }
        [Required]
        public int Type { get; set; }
    }
}
