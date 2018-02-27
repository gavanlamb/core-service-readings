using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.ManagedClient;

namespace LuGa.Core.Services.Readings
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Topshelf;


    class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.Service<LuGaMqtt>();
                
                x.SetServiceName("luga-reader");
                x.SetDisplayName("luga-reader");
            });
        }
    }

}
