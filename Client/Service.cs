using Common;
using Common.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
	public class Service : IService
	{
		public IData GetData()
		{
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);

			Console.WriteLine("Connecting to server...\n");
			pipeClient.Connect();

			var p = new Protocol(){ MethodName = "GetData" };
			
			StreamHelper.WriteString(JsonConvert.SerializeObject(p), pipeClient);
			string json =  StreamHelper.ReadString(pipeClient);
			pipeClient.Close();

			return JsonConvert.DeserializeObject<Data>(json);
			
		}
	}

	public class Data : IData
	{
		public int Number { get; set; }
		public DateTime Time { get; set; }
	}
}
