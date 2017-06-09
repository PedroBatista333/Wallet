using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wallet.Web.Models.Enum;

namespace Wallet.Web.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 1)]
        [Index("description_index", IsUnique = true)]
        public string Description { get; set; }
        [Required]
        public ECategoryType Type { get; set; }
    }
}
