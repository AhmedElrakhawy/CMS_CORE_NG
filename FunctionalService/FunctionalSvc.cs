using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ModelService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalService
{
    public class FunctionalSvc : IFunctionalSvc
    {
        private readonly AdminUserOptions _adminUserOptions;
        private readonly AppUserOptions _appUserOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        public FunctionalSvc(IOptions<AppUserOptions> appUserOptions
            , IOptions<AdminUserOptions> adminUserOptions , 
            UserManager<ApplicationUser> userManager
            )
        {
            _appUserOptions = appUserOptions.Value;
            _adminUserOptions = adminUserOptions.Value;
            _userManager = userManager;
        }

        public async Task CreateDefaultAdminUser()
        {
            try
            {
                var adminUser = new ApplicationUser
                {
                    Email = _adminUserOptions.Email,
                    UserName = _adminUserOptions.UserName,
                    EmailConfirmed = true,
                    ProfilePic = GetDefaultProfilePic(),
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    FirstName = _adminUserOptions.FirstName,
                    LastName = _adminUserOptions.LastName,
                    UserRole = "Adminstrator",
                    IsActive = true,
                    UserAddresses = new List<AddressModel>
                    {
                        new AddressModel{ Country = _adminUserOptions.Country , Type = "Billing"},
                        new AddressModel{Country = _adminUserOptions.Country , Type = "Shipping"}
                    }
                };

                var result = await _userManager.CreateAsync(adminUser, _adminUserOptions.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Adminstrator");

                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetDefaultProfilePic()
        {
            return string.Empty; 
        }

        public async Task CreateDefaultUser()
        {
            try
            {
                var appUser = new ApplicationUser
                {
                    Email = _adminUserOptions.Email,
                    UserName = _adminUserOptions.UserName,
                    EmailConfirmed = true,
                    ProfilePic = GetDefaultProfilePic(),
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    FirstName = _adminUserOptions.FirstName,
                    LastName = _adminUserOptions.LastName,
                    UserRole = "Customer",
                    IsActive = true,
                    UserAddresses = new List<AddressModel>
                    {
                        new AddressModel{ Country = _adminUserOptions.Country , Type = "Billing"},
                        new AddressModel{Country = _adminUserOptions.Country , Type = "Shipping"}
                    }
                };

                var result = await _userManager.CreateAsync(appUser, _adminUserOptions.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Customer");

                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
