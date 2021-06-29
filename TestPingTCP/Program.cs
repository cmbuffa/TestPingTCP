using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TestPingTCP
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var retval = await PingHttp();
            var retval = Ping();
            Console.WriteLine(retval);
            Console.ReadLine();
        }

        static int Ping()
        {
            try
            {
                string hostName = "stackoverflow.com";
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Blocking = true;

                socket.BeginConnect(hostName, 443, null, null).AsyncWaitHandle.WaitOne(300, true);  //"pvalue-dev.auth0.com"

                if (!socket.Connected)
                {
                    return 400;
                }

                return 200;
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
                return 400;
            }
        }

        static async Task<int> PingHttp() 
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.Timeout = TimeSpan.FromMilliseconds(2000);
                var uri = new UriBuilder("https", "pvalue-dev.auth0.com").Uri;
                var result = await Client.GetAsync(uri); //"http://pvalue-dev.auth0.com"
                int StatusCode = (int)result.StatusCode;
                return StatusCode;
            }
            catch(HttpRequestException re)
            {
                Console.WriteLine(re.Message);
                return 400;
            }
            catch(InvalidOperationException iop)
            {
                Console.WriteLine(iop.Message);
                return 500;
            }
            catch(UriFormatException ue)
            {
                Console.WriteLine(ue.Message);
                return 400;
            }
            catch(TaskCanceledException te)
            {
                Console.WriteLine(te.Message);
                return 400;
            }
            catch(OperationCanceledException oe)
            {
                Console.WriteLine(oe.Message);
                return 400;
            }
        }
    }
}
