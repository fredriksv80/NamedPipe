using Common;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			IService sevices = new Service();

			IData d = sevices.GetData();
			Console.WriteLine(d.Time.ToString());
			Thread.Sleep(2000);
			d = sevices.GetData();
			Console.WriteLine(d.Time.ToString());
			Thread.Sleep(2000);
		}
	}


}

