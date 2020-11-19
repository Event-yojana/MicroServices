namespace EventYojana.API.DataAccess.DataEntities.Common
{
    public class RegisterUser
    {
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int UserType { get; set; }
    }
}
