using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using ShadowTracker.Models;
using ShadowTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowTracker.Data
{
    public static class DataUtility
    {
        private static int company1Id;
        private static int company2Id;
        private static int company3Id;
        private static int company4Id;
        private static int company5Id;

        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);

            var userInfo = databaseUri.UserInfo.Split(':');
        
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };

            return builder.ToString();
        }

        public static async Task ManageDataAsync(IHost host)
        {
            using var svcScope = host.Services.CreateScope();

            var svcProvider = svcScope.ServiceProvider;

            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            var roleManagerSvc = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManagerSvc = svcProvider.GetRequiredService<UserManager<BTUser>>();
            
            await dbContextSvc.Database.MigrateAsync();

            //Things we need to create:
            //Roles
            await SeedRolesAsync(roleManagerSvc);
            //Companies
            await SeedCompaniesAsync(dbContextSvc);
            //Users
            await SeedUsersAsync(userManagerSvc);
            //Demo Users
            //await SeedDemoUsersAsync(userManagerSvc);
            //Project Priorities
            await SeedProjectPrioritiesAsync(dbContextSvc);
            //Ticket Statuses
            await SeedTicketStatusesAsync(dbContextSvc);
            //Ticket Priorities
            await SeedTicketPrioritiesAsync(dbContextSvc);
            //Ticket Types
            await SeedTicketTypesAsync(dbContextSvc);
            //Notification Types
            //await SeedNotificationTypes(dbContextSvc);
            //Projects
            await SeedProjectsAsync(dbContextSvc);
            //Tickets
            await SeedTicketsAsync(dbContextSvc);

        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManagerSvc)
        {
            await roleManagerSvc.CreateAsync(new IdentityRole(BTRoles.Admin.ToString()));
            await roleManagerSvc.CreateAsync(new IdentityRole(BTRoles.ProjectManager.ToString()));
            await roleManagerSvc.CreateAsync(new IdentityRole(BTRoles.Developer.ToString()));
            await roleManagerSvc.CreateAsync(new IdentityRole(BTRoles.Submitter.ToString()));
            await roleManagerSvc.CreateAsync(new IdentityRole(BTRoles.DemoUser.ToString()));
        }

        private static async Task SeedCompaniesAsync(ApplicationDbContext dbContextSvc)
        {
            try
            {
                IList<Company> defaultCompanies = new List<Company>()
                {
                    new Company() {Name = "Company 1", Description = "This is default Company 1"},
                    new Company() {Name = "Company 2", Description = "This is default Company 2"},
                    new Company() {Name = "Company 3", Description = "This is default Company 3"},
                    new Company() {Name = "Company 4", Description = "This is default Company 4"},
                    new Company() {Name = "Company 5", Description = "This is default Company 5"}
                };

                var dbCompanies = dbContextSvc.Companies.Select(c => c.Name).ToList();

                await dbContextSvc.Companies.AddRangeAsync(defaultCompanies.Where(c => !dbCompanies.Contains(c.Name)));

                await dbContextSvc.SaveChangesAsync();

                company1Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 1").Id;
                company2Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 2").Id;
                company3Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 3").Id;
                company4Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 4").Id;
                company5Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 5").Id;
            }
            catch(Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Companies");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }
        }

        private static async Task SeedUsersAsync(UserManager<BTUser> userManagerSvc)
        {
            var defaultUser = new BTUser
            {
                UserName = "btadmin1@bugtracker.com",
                Email = "btadmin1@bugtracker.com",
                FirstName = "Bill",
                LastName = "AppUser",
                EmailConfirmed = true,
                CompanyId = company1Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if(user is null)
                {
                    //When seeding users you MUST meet password complexity requirements
                    //6 char minimum, 1 upper, 1 lower, 1 number, 1 special
                    //If you do not meet the requirement the user will not be create
                    //but no error will be thrown either
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Admin.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Admin User 1");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }

            defaultUser = new BTUser
            {
                UserName = "btadmin2@bugtracker.com",
                Email = "btadmin2@bugtracker.com",
                FirstName = "Georgina",
                LastName = "AppUser",
                EmailConfirmed = true,
                CompanyId = company2Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Admin.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Admin User 2");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }

            //Seed 2 Project Manager users 1 for company1 and 1 for company2
            //Seed 6 Developers 3 per company
            //Seed 6 Submitters 3 per company

        }

        private static async Task SeedDemoUsersAsync(UserManager<BTUser> userManagerSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedProjectPrioritiesAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedTicketStatusesAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedTicketPrioritiesAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedTicketTypesAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedNotificationTypes(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedProjectsAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedTicketsAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }
    }
}
