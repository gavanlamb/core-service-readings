using System;
using System.Text;
using System.Threading.Tasks;
using LuGa.Core.Device.Models;
using LuGa.Core.Repository;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.ManagedClient;
using Topshelf;
using System.Diagnostics;

namespace LuGa.Core.Services.Readings
{
    public class LuGaMqtt : ServiceControl
    {
        private readonly IManagedMqttClient _client;
        private readonly IRepository<Reading> readingRepository;

        public LuGaMqtt(
            LuGaMqttConfig config,
            IRepository<Reading> readingRepository)
        {
            this.readingRepository = readingRepository;

            var factory = new MqttFactory();

            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(Constants.ReconnectDelay))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(config.ClientId)
                    .WithTcpServer(config.Host, config.Port)
                    .WithCredentials(config.Username, config.Password)
                    .Build())
                .Build();

            _client = factory.CreateManagedMqttClient();

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

                readingRepository.Add(reading);
            };

            _client.Connected += async (s, e) =>
            {
                Debug.WriteLine(Constants.ConnectedOutput);
                await _client.SubscribeAsync(
                    new TopicFilterBuilder()
                        .WithTopic(Constants.SubscribeTopic)
                        .Build()
                );
            };

            _client.Disconnected += (s, e) => {
                Debug.WriteLine(Constants.DisconnectedOutput);
            };

            Task.Run(() => Background(options));
        }

        public bool Start(HostControl hostControl)
        {
            Debug.WriteLine(Constants.StartedOutput);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Debug.WriteLine(Constants.StoppedOutput);

            return true;
        }

        async Task Background(ManagedMqttClientOptions options)
        {
            await _client.StartAsync(options);
        }
    }
}