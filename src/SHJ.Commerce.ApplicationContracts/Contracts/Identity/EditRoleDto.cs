﻿using SHJ.BaseFramework.Shared;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity
{
    public class EditRoleDto : BaseDto
    {
        public EditRoleDto()
        {
            Permissions = new();
        }
        [Required, MaxLength(256)]
        public string Name { get; set; } = string.Empty;
        public List<Guid> Permissions { get; set; }
    }
}