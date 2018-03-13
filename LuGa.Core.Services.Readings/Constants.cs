using System;
using System.Collections.Generic;
using System.Text;

namespace LuGa.Core.Services.Readings
{
    public static class Constants
    {
        public const string Password = "mqtt:password";
        public const string Username = "mqtt:username";
        public const string ClientId = "mqtt:clientid";
        public const string Host = "mqtt:host";
        public const string Port = "mqtt:port";

        public const string ServiceName = "luga-reader";

        public const string ConnectionString = "connectionString";

        public const string Environment = "ASPNETCORE_ENVIRONMENT";

        public const string SplitCharacter = "/";

        public const string SubscribeTopic = "devices/#";
        public const string MessageTopic1 = "/value";
        public const string MessageTopic2 = "$";

        public const string ConnectedOutput = "### CONNECTED WITH SERVER ###";
        public const string DisconnectedOutput = "### DISCONNECTED WITH SERVER ###";
        public const string StartedOutput = "Started the service";
        public const string StoppedOutput = "Stopped the service";

        public const int ReconnectDelay = 5;
    }
}
