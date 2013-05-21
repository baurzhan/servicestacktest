using System;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using TestData;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Logging;
using System.Text;

namespace TestData
{
    public class StatesResult
    {
        public List<TestDto> List { get; set;}
    }

    public class GetLastStates : IReturn<StatesResult> 
    {
        public DateTime Since {get;set;}
    }
    public class GetLastStatesSince : IReturn<List<TestDto>> 
    {
        public DateTime Since {get;set;}
    }
    public class TestService : Service
    {

        private  StatesResult result = new StatesResult();

        //private ILogger log;

        public TestService ()
        {

            result.List = new List<TestDto>();
            StringBuilder bigData = new StringBuilder(8192);
            for (int i = 0; i < 1000; i++)
            {
                bigData.Append("Some text");
            }
            for (int i = 0; i < 4000; i++)
            {
                result.List.Add(new TestDto { Id = i, Date = DateTime.UtcNow, Latitude = 43.0, Longitude = 76.5, BigData = bigData.ToString()});
            }
            ILog log = this.TryResolve<ILog>();
            if (log != null)
                log.Debug("Service constructor");
        }

        public StatesResult Get(GetLastStates request)
        {
            return result;
        }


        public List<TestDto> Get(GetLastStatesSince request)
        {
            List<TestDto> list = new List<TestDto>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new TestDto { Id = i, /*Date = request.Since.AddSeconds(i),*/ Latitude = 43.0, Longitude = 76.5});
            }
            return list;
        }

    }
    public class StateServerHost : AppHostHttpListenerLongRunningBase 
    {
        public StateServerHost() : base("HttpListener", typeof(TestService).Assembly) 
        {
            LogManager.LogFactory = new ServiceStack.Logging.Support.Logging.ConsoleLogFactory();
        }

        public override void Configure(Funq.Container container) {
            this.SetConfig(new EndpointHostConfig {DebugMode = true});

        }
    }
}

