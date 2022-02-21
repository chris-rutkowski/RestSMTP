namespace RestSMTP.Dtos
{
    public class EmailDto
    {
        // optional
        public string FromName { get; set; }

        // required
        public string FromEmail { get; set; }

        // required
        public string To { get; set; }

        // optional
        public string ReplyTo { get; set; }

        // required, min 1 non-whitespace character
        public string Subject { get; set; }

        // optional, no limits
        public string Content { get; set; }

        // default false
        public bool IsContentHTML { get; set; }
    }
}
