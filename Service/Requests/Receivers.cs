namespace Service.Requests
{
    public class Receivers
    {
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string>? Cc { get; set; }
        public IEnumerable<string>? Bcc { get; set; }

    }
}
