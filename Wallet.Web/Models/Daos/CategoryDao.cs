using System.Linq;
using System;
using System.Collections.Generic;
using Wallet.Web.Infrastructure.Context;
using System.Threading.Tasks;
using System.Data.Entity;
using Wallet.Web.Models.Enum;

namespace Wallet.Web.Models.Daos
{
    public class CategoryDao : BaseDao<Category>
    {
        public CategoryDao(WalletContext context) :
            base(context)
        { }

        public async Task<IEnumerable<Category>> GetByCategoryTypeAsync(ECategoryType categoryType)
        {
            try
            {
                var list = await (from c in db.Categories
                                  where c.Type == categoryType
                                  orderby c.Description
                                  select c).ToListAsync();

                return list;
            }
            catch (Exception e) { }

            return new List<Category>();
        }
    }
}
