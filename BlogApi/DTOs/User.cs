namespace BlogApi.DTOs
{
    public class User
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
