using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interface;

namespace Common.Implementation
{
	public class Services : IService
	{
		private IData data;
		public Services()
		{
			data = new Data();

			data.Number = 2;
			data.Time = DateTime.Now;

		}
		public IData GetData()
		{
			return data;
		}
	}
}
