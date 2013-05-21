using System;
using TestData;

namespace ServiceStackServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            StateServerHost host = new StateServerHost();
            host.Init();
            host.Start( "http://127.0.0.1:8888/" );
            Console.Read();
            host.Stop();
        }
    }
}
