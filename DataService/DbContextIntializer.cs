using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionalService;

namespace DataService
{
    public static class DbContextIntializer
    {
        public static async Task Intialize(DataProtectionKeysContext dataProtectionKeysContext,
            ApplicationDbContext applicationDbContext,
            IFunctionalSvc functionalSvc)
        {
            //ensure that the app Protection Keys Context Database is Created
            await dataProtectionKeysContext.Database.EnsureCreatedAsync();

            //ensure that the applicationDbcontext database is created
            await applicationDbContext.Database.EnsureCreatedAsync();

            //ensure that we have users 
            if (applicationDbContext.ApplicationUsers.Any())
            {
                return;
            }

            //if empty create admin user and app user 

            await functionalSvc.CreateDefaultAdminUser();
            await functionalSvc.CreateDefaultUser();
        }
    }
}
