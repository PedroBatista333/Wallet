using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wallet.Web.Models;
using System;
using Wallet.Web.Models.Daos;
using Wallet.Web.ViewModels;
using Wallet.Web.Mapping;
using System.Collections.Generic;
using Wallet.Web.Models.Enum;
using Wallet.Web.Infrastructure.Context;

namespace Wallet.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly WalletContext _context;

        public CategoryController(WalletContext context)
        {
            _context = context;
        }

        [HttpGet("category/{categoryType}")]
        public async Task<IEnumerable<Category>> GetByCategoryTypeAsync(int categoryType)
        {
            CategoryDao categoryDao = new CategoryDao(_context);

            try
            {
                return await categoryDao.GetByCategoryTypeAsync((ECategoryType) categoryType);
            }
            catch (Exception e) { }

            return new List<Category>();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryViewModel categoryVm)
        {
            Category category = new Category();
            CategoryDao categoryDao = new CategoryDao(_context);

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                category = CategoryViewModelToCategory.Map(categoryVm);

                category.CategoryId = await categoryDao.AddAsync(category);

            }
            catch (Exception e) { }

            return Created("category", category);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            CategoryDao categoryDao = new CategoryDao(_context);

            try
            {
                await categoryDao.AddAsync(new Category() { CategoryId = id });
            }
            catch (Exception e) { }

            return;
        }
    }
}
