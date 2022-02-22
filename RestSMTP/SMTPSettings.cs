namespace RestSMTP
{
    public class SMTPSettings
    {
        public string Host { get; set; } = String.Empty;
        public int Port { get; set; } = 587;
        public bool SSL { get; set; } = true;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;    }
}
