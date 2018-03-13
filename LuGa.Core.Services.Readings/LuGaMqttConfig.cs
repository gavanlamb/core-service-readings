namespace LuGa.Core.Services.Readings
{
    public class LuGaMqttConfig
    {
        public LuGaMqttConfig(
            string username,
            string password,
            string clientId,
            string host,
            int port)
        {
            Username = username;
            Password = password;
            ClientId = clientId;
            Host = host;
            Port = port;
        }

        public string ClientId { get; }

        public string Host { get; }

        public int Port { get; }

        public string Username { get; }

        public string Password { get; }
    }
}
