using System.Text.Encodings.Web;
using Wallet.Web.Models;
using Wallet.Web.Models.Enum;
using Wallet.Web.ViewModels;

namespace Wallet.Web.Mapping
{
    public class CategoryViewModelToCategory
    {
        public static Category Map(CategoryViewModel categoryVm)
        {
            string description = HtmlEncoder.Default.Encode(categoryVm.Description);

            return new Category
            {
                Description = description,
                Type = (ECategoryType) categoryVm.Type
            };
        }
    }
}
