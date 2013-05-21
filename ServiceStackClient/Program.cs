using System;
using ServiceStack.ServiceClient.Web;
using TestData;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ServiceStackClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            JsonServiceClient client = new JsonServiceClient("http://127.0.0.1:8888/");
            client.Timeout = TimeSpan.FromSeconds(60);
            client.ReadWriteTimeout = TimeSpan.FromSeconds(60);
            client.DisableAutoCompression = true;
            int success = 0, error = 0;
            Parallel.For(0, 100, i => {
                try
                {
                    Console.WriteLine(i.ToString());
                    StatesResult result = client.Get(new GetLastStates());
                    if(result.List.Count < 4000)
                        error++;
                    Console.WriteLine("Received " + result.List.Count + " items");
                    success++;
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    error++;
                }
            });
            Console.WriteLine(string.Format("Test completed. Success count:{0}; Error count:{1}",success,error));
            client.Dispose();
        }
    }




}
