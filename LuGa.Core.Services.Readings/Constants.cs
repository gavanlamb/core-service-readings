using System;
using System.Collections.Generic;
using System.Text;

namespace LuGa.Core.Services.Readings
{
    public static class Constants
    {
        public static string Password = "mqtt:password";
        public static string Username = "mqtt:username";
        public static string ClientID = "mqtt:clientid";
        public static string Host = "mqtt:host";
        public static string Port = "mqtt:port";

        public static string ServiceName = "luga-reader";

        public static string ConnectionString = "connectionString";

        public static string Environment = "ASPNETCORE_ENVIRONMENT";

        public static string SplitCharacter = "/";

        public static string SubscribeTopic = "devices/#";
        public static string MessageTopic1 = "/value";
        public static string MessageTopic2 = "$";

        public static string ConnectedOutput = "### CONNECTED WITH SERVER ###";
        public static string DisconnectedOutput = "### DISCONNECTED WITH SERVER ###";
        public static string StartedOutput = "Started the service";
        public static string StoppedOutput = "Stopped the service";

        public static int ReconnectDelay = 5;
    }
}
