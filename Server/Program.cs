using Common;
using Common.Implementation;
using Common.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
	class Program
	{
		private static int numThreads = 4;
		

		private static IService services = new Common.Implementation.Services();

		static void Main(string[] args)
		{
			int i;
			Thread[] servers = new Thread[numThreads];

			Console.WriteLine("Waiting for client connect...\n");
			for (i = 0; i < numThreads; i++)
			{
				servers[i] = new Thread(ServerThread);
				servers[i].Start();
			}
			Thread.Sleep(250);
			while (i > 0)
			{
				for (int j = 0; j < numThreads; j++)
				{
					if (servers[j] != null)
					{
						if (servers[j].Join(250))
						{
							Console.WriteLine("Server thread[{0}] finished.", servers[j].ManagedThreadId);
							servers[j] = null;
							i--;    // decrement the thread watch count
						}
					}
					else
					{
						//Starta en ny tråd
						servers[j] = new Thread(ServerThread);
						servers[j].Start();
						i++;
					}
				}
			}
			Console.WriteLine("\nServer threads exhausted, exiting.");
		}

		//private class Data : IData
		//{
		//	public int Number { get; set; }
		//	public DateTime Time { get; set; }
			
		//}

		private static void ServerThread()
		{
			NamedPipeServerStream pipeServer =
				new NamedPipeServerStream("testpipe", PipeDirection.InOut, numThreads);

			int threadId = Thread.CurrentThread.ManagedThreadId;

			// Wait for a client to connect
			pipeServer.WaitForConnection();

			Console.WriteLine("Client connected on thread[{0}].", threadId);
			try
			{
				string json="";
				string function = StreamHelper.ReadString(pipeServer);

				switch (function)
				{
					case "GetData":

						IData data = services.GetData();
						data.Time = DateTime.Now; //Vill se att det sker senare
						json = JsonConvert.SerializeObject(data);

						break;
					default:
						throw new Exception("Unknow function");
				}


				StreamHelper.WriteString(json, pipeServer);
				
			}
			// Catch the IOException that is raised if the pipe is broken
			// or disconnected.
			catch (IOException e)
			{
				Console.WriteLine("ERROR: {0}", e.Message);

			}
			pipeServer.Close();

		}

		
	}

}

