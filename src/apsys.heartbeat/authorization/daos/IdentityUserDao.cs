namespace apsys.heartbeat.authorization.daos
{
    public class IdentityUserDao
    {
        public IdentityUserDao()
        { }

        public IdentityUserDao(string id, string userName, string name, string email, string userType, bool twoFactorEnabled, bool emailConfirmed)
        {
            Id = id;
            UserName = userName;
            Name = name;
            Email = email;
            UserType = userType;
            TwoFactorEnabled = twoFactorEnabled;
            EmailConfirmed = emailConfirmed;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
    }
}
