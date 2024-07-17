
namespace Pipeline.Testing.Pages.Settings.Users
{
    public class UserData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public UserData()
        {
            Username = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
            firstname = string.Empty;
            lastname = string.Empty;
        }

        public UserData(UserData data)
        {
            Username = data.Username;
            Email = data.Email;
            Role = data.Role;
            firstname = data.firstname;
            lastname = data.lastname;
        }
    }
}
