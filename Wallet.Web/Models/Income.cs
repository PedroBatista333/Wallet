using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Web.Models
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string Description { get; set; }
        [Required]
        public decimal Value { get; set; }
        [ForeignKey("CategoryId")]
        [Required]
        public Category Category { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Index("created_date_index")]
        public DateTime CreatedDate { get; set; }
    }
}
