using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity
{
    public class UserDto : BaseDto<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Job { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string? Mobile { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public decimal? Wallet { get; set; } = 0;
        public DateTime? DateOfBirth { get; set; }
        public string? CompanyName { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
    }
}