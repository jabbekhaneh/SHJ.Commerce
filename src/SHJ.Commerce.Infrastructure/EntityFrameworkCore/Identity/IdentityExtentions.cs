using Microsoft.IdentityModel.Tokens;
using SHJ.Commerce.Domain;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Shared.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Microsoft.AspNetCore.Identity;

public static class IdentityExtentions
{
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

            await userManager.CreateAsync(user, UserAdminInfo.AdminPasswordDefaultValue);
            await userManager.AddToRoleAsync(user, UserAdminInfo.RoleName);
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

    public static string GenerateToken(this SignInManager<User> SignInManager, User user,int ExpiresDay = 1)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(PortalConsts.SecurityKey);
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
}
