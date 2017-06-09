using System.Text.Encodings.Web;
using Wallet.Web.Models;
using Wallet.Web.ViewModels;

namespace Wallet.Web.Mapping
{
    public class IncomeViewModelToIncome
    {
        public static Income Map(IncomeViewModel incomeVm)
        {
            string description = HtmlEncoder.Default.Encode(incomeVm.Description);

            return new Income
            {
                Description = description,
                Value = incomeVm.Value,
                Category = new Category() { CategoryId = incomeVm.Category }
            };
        }
    }
}
