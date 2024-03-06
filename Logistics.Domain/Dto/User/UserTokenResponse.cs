namespace Logistics.Domain.Dto.User
{
    public class UserTokenResponse
    {
        public IEnumerable<UserClaimsResponse> Claims { get; set; }
    }
}
