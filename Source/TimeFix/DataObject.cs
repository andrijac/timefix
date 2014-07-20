using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFix
{
	public class DataObject
	{
		public string status
		{
			get;
			set;
		}

		public string message
		{
			get;
			set;
		}

		public string countryCode
		{
			get;
			set;
		}

		public string zoneName
		{
			get;
			set;
		}

		public string abbreviation
		{
			get;
			set;
		}

		public int gmtOffset
		{
			get;
			set;
		}

		public string dst
		{
			get;
			set;
		}

		public int timestamp
		{
			get;
			set;
		}
	}
}