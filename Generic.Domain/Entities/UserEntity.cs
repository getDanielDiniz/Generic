namespace Generic.Domain.Entities
{
    public class UserEntity : GenericBaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid SecurityId { get; set; }
        public bool IsActive { get; set; }
    }
}
