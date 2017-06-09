using System;
using Wallet.Web.Infrastructure.Context;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Wallet.Web.Models.Daos
{
    public abstract class BaseDao<T> where T : class
    {
        protected readonly WalletContext db;

        public BaseDao(WalletContext context)
        {
            db = context;
        }

        public async Task<int> AddAsync(T entity)
        {
            try
            {
                db.Set<T>().Add(entity);

                return await db.SaveChangesAsync();
            } catch (Exception e) { }

            return 0;
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);

                await db.SaveChangesAsync();
            } catch (Exception e) { }

            return;
        }
    }
}
