using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCloud.Manager.Demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GameCloud.Manager.Demo.Data
{
    public class DBInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Create default admin role and admin account
        public async void Initialize()
        {
            _context.Database.EnsureCreated();

            string defaultAdminRoleName = "Admin";
            var adminRole = await _roleManager.FindByNameAsync(defaultAdminRoleName);
            if (adminRole == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(defaultAdminRoleName));
            }

            string adminUserName = "admin@test.com";
            string adminPassword = "Password02@";
            await _userManager.CreateAsync(new ApplicationUser{UserName = adminUserName, Email = adminUserName}, adminPassword);
            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(adminUserName), defaultAdminRoleName);
        }
    }
}
