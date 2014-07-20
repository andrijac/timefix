using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeFix.Lib;

namespace TimeFixLocal
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string sDateTime = args[0];
			string sOffset = args[1];

			int offset = int.Parse(sOffset);

			DateTime dateTime = DateTime.Parse(sDateTime);

			dateTime = dateTime.AddHours(offset);

			SystemTime.ChangeSystemDateTime(dateTime);
		}
	}
}