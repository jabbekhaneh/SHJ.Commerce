using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Shared.Common;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[BaseControllerName("Permission")]
public class PermissionAppServices : BaseAppService, IPermissionAppServices
{
    private readonly PermissionManager _Manager;
    
    public PermissionAppServices(PermissionManager permissionManager)
    {
        _Manager = permissionManager;
        
    }

    [HttpGet]
    public async Task<BaseResult> Get()
    {
        var permissions = await _Manager.Permissions()
            .Select(_ => new PermissionDto
            {
                Id = _.Id,
                Name = _.Name,
                DisplayName = _.DisplayName,
                ParentId = _.ParentId,
                ParentName = _.ParentName,
            }).ToListAsync();

        return await ReturnResultAsync(permissions); ;
    }
}
