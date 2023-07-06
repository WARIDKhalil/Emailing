namespace Service.Requests
{
    public class EmailRequest
    {
        public string Subject { get; set; }
        public Receivers Receivers { get; set; }
        public EmailBody Body { get; set; }
    }
}
