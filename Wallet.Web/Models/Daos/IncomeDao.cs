using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using Wallet.Web.Infrastructure.Context;

namespace Wallet.Web.Models.Daos
{
    public class IncomeDao : BaseDao<Income>
    {
        public IncomeDao(WalletContext context) : 
            base(context)
        { }

        public async Task<IEnumerable<IGrouping<int, Income>>> GetByMonthAsync(int month, int year)
        {            
            try
            {
                DateTime startDate = DateTime.Parse(year + "-" + month + "-1 00:00");
                DateTime endDate = DateTime.Parse(year + "-" + month + "-31 23:59");

                var list = await (from i in db.Incomes
                                  where i.CreatedDate >= startDate && i.CreatedDate <= endDate
                                  orderby i.CreatedDate
                                  group i by i.CreatedDate.Day into g
                                  select g).ToListAsync();

                return list;
            }
            catch (Exception e) { }

            return new List<IGrouping<int, Income>>();
        }

        public async Task<decimal> GetBalanceByMonthAsync(int month, int year)
        {
            try
            {
                DateTime startDate = DateTime.Parse(year + "-" + month + "-1 00:00");
                DateTime endDate = DateTime.Parse(year + "-" + month + "-31 23:59");

                var list = await (from i in db.Incomes
                                  where i.CreatedDate >= startDate && i.CreatedDate <= endDate
                                  select new { i.Value }).SumAsync(x => x.Value);

                return list;
            }
            catch (Exception e) { }

            return 0;
        }

        public async Task<decimal> GetIncomeByMonthAsync(int month, int year)
        {
            try
            {
                DateTime startDate = DateTime.Parse(year + "-" + month + "-1 00:00");
                DateTime endDate = DateTime.Parse(year + "-" + month + "-31 23:59");

                var list = await (from i in db.Incomes
                                  where i.CreatedDate >= startDate && i.CreatedDate <= endDate && i.Value > 0
                                  select new { i.Value }).SumAsync(x => x.Value);

                return list;
            }
            catch (Exception e) { }

            return 0;
        }

        public async Task<decimal> GetExpenseByMonthAsync(int month, int year)
        {
            try
            {
                DateTime startDate = DateTime.Parse(year + "-" + month + "-1 00:00");
                DateTime endDate = DateTime.Parse(year + "-" + month + "-31 23:59");

                var list = await (from i in db.Incomes
                                  where i.CreatedDate >= startDate && i.CreatedDate <= endDate && i.Value < 0
                                  select new { i.Value }).SumAsync(x => x.Value);

                return list;
            }
            catch (Exception e) { }

            return 0;
        }

        public async Task<IEnumerable<object>> GetCategoriesMostUsedByMonthAsync(int month, int year)
        {
            try
            {
                DateTime startDate = DateTime.Parse(year + "-" + month + "-1 00:00");
                DateTime endDate = DateTime.Parse(year + "-" + month + "-31 23:59");

                var list = await (from i in db.Incomes
                                  where i.CreatedDate >= startDate && i.CreatedDate <= endDate && i.Value > 0
                                  orderby i.Category.Description
                                  group i by i.Category.Description into g
                                  select new { key = g.Key, total = g.Count() }).ToListAsync();

                return list;
            }
            catch (Exception e) { }

            return new List<object>();
        }
    }
}
