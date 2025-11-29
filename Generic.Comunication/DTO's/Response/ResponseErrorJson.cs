namespace Generic.Comunication.DTO_s.Response
{
    public class ResponseErrorJson
    {
        public List<string> Errors { get; set; }

        public ResponseErrorJson(List<string> errors)
        {
            Errors = errors;
        }
    }
}
