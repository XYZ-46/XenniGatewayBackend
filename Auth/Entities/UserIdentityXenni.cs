namespace Auth.Entities
{
    public class UserIdentityXenni
    {
        public long Id { get; set; }
        public string Email { get; private set; } = string.Empty;
        public string Role { get; set; } = "Intruder";
        public bool IsActive { get; private set; } = true;
    }
}
