namespace Service.Settings
{
    public class EmailSettings
    {
        /// <summary>
        /// Server Infos
        /// </summary>
        public string Server { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// Sender Infos
        /// </summary>
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
