using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Implementation
{
	public class Data : IData
	{
		private int _number;
		private DateTime _time;
		public int Number
		{
			get
			{
				return _number;
			}

			set
			{
				_number = value;
			}
		}

		public DateTime Time
		{
			get
			{
				return _time;
			}

			set
			{
				_time = value;
			}
		}
	}
}
