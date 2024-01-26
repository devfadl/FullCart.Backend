using FullCart.Application.Common.Mappings;

namespace FullCart.Application.Common.Dto;
public class UserBriefDto : IMapFrom<Domain.Entities.User>
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string ThirdName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName
    {
        get
        {
            return $"{FirstName} {SecondName} {ThirdName} {LastName}";
        }
        set
        {
            FullName = value;
        }
    }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public List<UserGroupBriefDto> UserGroups { get; set; }
}