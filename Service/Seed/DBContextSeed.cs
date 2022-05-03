using Data.DatabaseContext;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Seed
{
    public class DBContextSeed
    {
        private static IEnumerable<User> GetPreconfiguredOrders(IEncrypter encrypter)
        {
            var list = new List<User>();
            var salt = encrypter.GetSalt();
            var Admin = new User
            {
                Email = "admin@gmail.com",
                Salt = salt,
                PasswordHash = encrypter.GetHash("password", salt),
                CreateAt = DateTime.Now,
                Role = Data.Enums.Role.Administrator,
            };
            list.Add(Admin);

            return list;
        }

        public static async Task SeedAsync(CRMDbContext dbContext,
            IEncrypter encrypter, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // INFO: Run this if using a real database. Used to automaticly migrate docker image of sql server db.
                dbContext.Database.Migrate();
                //orderContext.Database.EnsureCreated();

                var users = dbContext.Set<User>();

                if (!users.Any())
                {
                    users.AddRange(GetPreconfiguredOrders(encrypter));
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 5)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CRMDbContext>();
                    log.LogError(exception.Message);
                    await SeedAsync(dbContext, encrypter, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }
    }
}
