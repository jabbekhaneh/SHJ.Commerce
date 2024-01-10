using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity
{
    public class UsersDto : BaseDto
    {
        public UsersDto()
        {
            Users = new List<UserDto>();
        }
        public List<UserDto> Users { get; set; }
        public int PageSize { get; set; }
    }
}