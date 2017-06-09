using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Web.Infrastructure.Context
{
    public class WalletContextFactory : IDbContextFactory<WalletContext>
    {
        public WalletContext Create()
        {
            return new WalletContext("Server=(localdb)\\mssqllocaldb;Database=WalletDb;MultipleActiveResultSets=true");
        }
    }
}
