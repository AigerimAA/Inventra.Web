namespace Inventra.Domain.ValueObjects
{
    public class UserLookup
    {
        public string Id { get; } 
        public string UserName { get;} 
        public string Email { get; }

        public UserLookup(string id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
        }
    }
}
