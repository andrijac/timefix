using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using TimeFix.Lib;

namespace TimeFixP
{
	internal class Program
	{
		private static Timer aTimer;

		private static Timer checkTimer;

		private static DateTime mainDateTime;

		private static void Main(string[] args)
		{
			string sDateTime = args[0];
			string sOffset = args[1];

			int offset = int.Parse(sOffset);

			mainDateTime = DateTime.Parse(sDateTime);

			mainDateTime = mainDateTime.AddHours(offset);

			SystemTime.ChangeSystemDateTime(mainDateTime);

			aTimer = new System.Timers.Timer(1000);
			aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			aTimer.Start();

			checkTimer = new Timer(1000 * 60 * 2);
			checkTimer.Elapsed += new ElapsedEventHandler(CheckTimedEvent);
			checkTimer.Start();

			Console.WriteLine("Fixing time...");
			Console.Read();
		}

		private static void CheckTimedEvent(object source, ElapsedEventArgs e)
		{
			TimeSpan timeSpan1 = DateTime.Now - mainDateTime;
			TimeSpan timeSpan2 = mainDateTime - DateTime.Now;

			Console.WriteLine("Fixing time {0} ...", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

			if (timeSpan1.TotalMinutes > 5
				|| timeSpan1.TotalMinutes < 5
				|| timeSpan2.TotalMinutes > 5
				|| timeSpan2.TotalMinutes < 5)
			{
				SystemTime.ChangeSystemDateTime(mainDateTime);
				Console.WriteLine(string.Empty);
				Console.WriteLine("Time fixed to {0}", mainDateTime.ToString("yyyy-MM-dd HH:mm"));
				Console.WriteLine(string.Empty);
			}
		}

		// Specify what you want to happen when the Elapsed event is raised.
		private static void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			mainDateTime = mainDateTime.AddSeconds(1);

			//// Console.WriteLine(mainDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
		}
	}
}