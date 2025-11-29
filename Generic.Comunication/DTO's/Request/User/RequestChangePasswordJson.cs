namespace Generic.Comunication.DTO_s.Request.User
{
    public class RequestChangePasswordJson
    {
        public string newPassword {  get; set; } = String.Empty;
        public string oldPassword { get; set; } = String.Empty;
    }
}
