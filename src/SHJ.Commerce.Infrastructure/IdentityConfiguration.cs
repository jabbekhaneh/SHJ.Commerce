using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SHJ.Commerce.Shared.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SHJ.Commerce.Domain;
using SHJ.ExceptionHandler;
using System.Collection;
using SHJ.Commerce.Domain.Shared;

namespace SHJ.Commerce.Infrastructure;

public static class IdentityConfiguration
{

    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {

        services.AddScoped<PermissionManager, PermissionManager>();

        services.AddIdentity<User, Role>(options =>
        {
            //options.Password.RequireDigit = true;
            //options.Password.RequireLowercase = true;
            //options.Password.RequireNonAlphanumeric = true;
            //options.Password.RequireUppercase = true;
            //options.Password.RequiredLength = 6;
            //options.Password.RequiredUniqueChars = 1;
            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(42000);
            //options.Lockout.MaxFailedAccessAttempts = 10;
            //options.Lockout.AllowedForNewUsers = true;
            //options.User.AllowedUserNameCharacters =
            //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;


        }).AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

        return services;
    }

    #region   Initialize data in identity 

    public static async Task InitializeUserAdminAsync(this UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                FirstName = UserAdminInfo.AdminFirstName,
                LastName = UserAdminInfo.AdminLastName,
                Email = UserAdminInfo.AdminEmailDefaultValue.ToLower(),
                UserName = UserAdminInfo.AdminUserNameDefaultValue.ToLower(),
                EmailConfirmed = true,
                MobileNumberConfirmed = true,
            };
            var addUserResult = await userManager.CreateAsync(user, UserAdminInfo.AdminPasswordDefaultValue);
            addUserResult.CheckErrors();
            var addRoleResult= await userManager.AddToRoleAsync(user, UserAdminInfo.RoleName);
            addRoleResult.CheckErrors();
        }
    }

    public static async Task InitializeRoleAdminAsync(this RoleManager<Role> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            var adminRole = new Role
            {
                Name = UserAdminInfo.RoleName,
            };
            await roleManager.CreateAsync(adminRole);
        }
    }

    public static async Task InitializePermissionsAsync(this PermissionManager permissionManager)
    {
        List<Permission> permissions = new();
        permissions.Add(new Permission("Permission", "Permission"));
        permissions.Add(new Permission("Role", "Role"));
        permissions.Add(new Permission("User", "User"));

        foreach (var permission in permissions)
        {
            await permissionManager.CreateAsync(permission);
        }
    }

    public static string GetClaim(this ClaimsPrincipal userClaimsPrincipal, string claimType)
    {
        return userClaimsPrincipal.Claims.FirstOrDefault((Claim x) => x.Type == claimType)?.Value ?? "";
    }

    public static string GenerateToken(this SignInManager<User> SignInManager, User user, int ExpiresDay = 1)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(BaseJwtConsts.SecurityKey);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName ?? ""),
                new Claim(ClaimTypes.Surname,user.LastName ?? ""),
                new Claim(ClaimTypes.MobilePhone,user.Mobile ?? ""),
            }),
            Expires = DateTime.UtcNow.AddDays(ExpiresDay),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                                  SecurityAlgorithms.HmacSha256Signature),
            //Audience=""
            //Issuer=""
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    #endregion



    #region IDENTITY RESULT CHECK
    public static void CheckErrors(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return;
        }

        if (identityResult.Errors == null)
        {
            throw new ArgumentException("identityResult.Errors is  null.");
        }

        throw new BaseBusinessException(
            code: "Identity Error Code : " + identityResult.Errors.First().Code,
            message: identityResult.Errors.Select(err => err.Description).JoinAsString(", "));
    }


    public static void CheckSignInResultErrors(this SignInResult signInResult)
    {
        if (signInResult.Succeeded)
            return;

        if (signInResult.IsLockedOut)
            throw new BaseBusinessException(GlobalIdentityErrors.IsLockedOut, "IsLockedOut");

        if (signInResult.IsNotAllowed)
            throw new BaseBusinessException(GlobalIdentityErrors.IsNotAllowed, "IsNotAllowed");

        if (signInResult.RequiresTwoFactor)
            throw new BaseBusinessException(GlobalIdentityErrors.RequiresTwoFactor, "RequiresTwoFactor");

        throw new BaseBusinessException(GlobalIdentityErrors.Name);
    }
    #endregion
    #region Private Methods


    private static IServiceCollection BuildCookieeBase(this IServiceCollection services)
    {
        //services.AddAuthentication();
        //services.AddAuthorization();

        services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, option =>
        {
            option.LogoutPath = "/";
            option.LogoutPath = "/";
            option.AccessDeniedPath = "/";
            option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            option.Cookie.Name = "SHJ.BaseCommerce.COOKIE";
        });
        return services;
    }
    #endregion
}
