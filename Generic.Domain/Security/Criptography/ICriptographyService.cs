namespace Generic.Domain.Security.Criptography
{
    public interface ICriptographyService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
