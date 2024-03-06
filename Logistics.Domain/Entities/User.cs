using Logistics.Domain.Entities.Base;

namespace Logistics.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string userName, string password)
        {
            Name = name;
            UserName = userName;
            Password = password;
        }

        public string Name { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
