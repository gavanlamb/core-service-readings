using System;
using System.Text;
using System.Threading.Tasks;
using LuGa.Core.Device.Models;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.ManagedClient;
using Topshelf;

namespace LuGa.Core.Services.Readings
{
    public class LuGaMqtt : ServiceControl
    {
        private readonly IManagedMqttClient _client;

        public LuGaMqtt()
        {

            var factory = new MqttFactory();

            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId("reader")
                    .WithTcpServer("mqtt.luga.online", 1883)
                    .WithCredentials("reader", "xxx")
                    .Build())
                .Build();

            _client = new MqttFactory().CreateManagedMqttClient();


            _client.ApplicationMessageReceived += (s, e) =>
            {
                if (e.ApplicationMessage.Topic.IndexOf("/value", StringComparison.Ordinal) <= -1 ||
                    e.ApplicationMessage.Topic.IndexOf("$", StringComparison.Ordinal) != -1) return;

                var reading = new Reading();
                var pulled = e.ApplicationMessage.Topic.Split("/");

                reading.DeviceId = pulled[1];
                reading.ReadingType = pulled[2];
                reading.Value = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                reading.TimeStamp = DateTime.UtcNow;

                Console.WriteLine();

            };

            _client.Connected += async (s, e) =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");
                await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic("devices/#").Build());
            };

            _client.Disconnected += async (s, e) => { Console.WriteLine("### DISCONNECTED WITH SERVER ###"); };

            Task.Run(() => Background(options));
        }

        public bool Start(HostControl hostControl)
        {
            Console.WriteLine("Started the service");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Console.WriteLine("Stopped the service");

            return true;
        }

        async Task Background(ManagedMqttClientOptions options)
        {
            await _client.StartAsync(options);
        }
    }
}