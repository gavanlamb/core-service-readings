namespace LuGa.Core.Services.Readings
{
    public class LuGaMqttConfig
    {
        public LuGaMqttConfig(
            string Username,
            string Password,
            string ClientId,
            string Host,
            int Port)
        {
            this.Username = Username;
            this.Password = Password;
            this.ClientId = ClientId;
            this.Host = Host;
            this.Port = Port;
        }

        public string ClientId { get; }

        public string Host { get; }

        public int Port { get; }

        public string Username { get; }

        public string Password { get; }
    }
}
