using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wallet.Web.Models;
using Wallet.Web.ViewModels;
using Wallet.Web.Mapping;
using Wallet.Web.Models.Daos;
using Wallet.Web.Infrastructure.Context;

namespace Wallet.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class IncomeController : Controller
    {
        private readonly WalletContext _context;

        public IncomeController(WalletContext context)
        {
            _context = context;
        }

        [HttpGet("getbymonth/{year}/{month}")]
        public async Task<object> GetByMonth(int year, int month)
        {
            IncomeDao incomeDao = new IncomeDao(_context);

            try
            {
                var _incomes = await incomeDao.GetByMonthAsync(year, month);
                var _balance = await incomeDao.GetBalanceByMonthAsync(year, month);
                var _totalIncome = await incomeDao.GetIncomeByMonthAsync(year, month);
                var _totalExpense = await incomeDao.GetExpenseByMonthAsync(year, month);
                var _categoryMostUsed = await incomeDao.GetCategoriesMostUsedByMonthAsync(year, month);

                return new {
                    balance = _balance,
                    totalIncome = _totalIncome,
                    totalExpense = _totalExpense,
                    categoryMostUsed = _categoryMostUsed,
                    incomes = _incomes
                };
            }
            catch (Exception e) { }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IncomeViewModel incomeVm)
        {
            Income income = new Income();
            IncomeDao incomeDao = new IncomeDao(_context);

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                income = IncomeViewModelToIncome.Map(incomeVm);

                income.IncomeId = await incomeDao.AddAsync(income);

            } catch(Exception e) { }

            return Created("income", income);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            IncomeDao incomeDao = new IncomeDao(_context);

            try
            {
                await incomeDao.AddAsync(new Income() { IncomeId = id });
            }
            catch (Exception e) { }

            return;
        }
    }
}
