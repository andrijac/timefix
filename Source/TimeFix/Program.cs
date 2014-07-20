using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

using System.Net.Http.Headers;

namespace TimeFix
{
	/// <summary>
	/// Timezone API:
	/// http://timezonedb.com/api
	/// Found it at:
	/// http://stackoverflow.com/a/13240557/84852
	///
	/// Code used:
	///
	/// REST call:
	/// http://stackoverflow.com/a/17459045/84852
	///
	/// Convert Unix timstamp to DateTime:
	/// http://stackoverflow.com/a/250400/84852
	/// </summary>
	public class Program
	{
		private static void Main(string[] args)
		{
			bool autoConfirm = bool.Parse(ConfigurationManager.AppSettings["autoConfirm"]);
			bool isConfirmed = false;

			TimeZoneDb timeZoneDb = new TimeZoneDb();

			DataObject dataObjects = timeZoneDb.GetData();

			if (dataObjects == null)
			{
				Console.WriteLine("Error in retrieving data from webservice.");
				return;
			}

			string outputPattern = "yyyy-MM-dd HH:mm:ss";

			if (dataObjects.status != "OK")
			{
				Console.WriteLine(dataObjects.message);
				return;
			}

			int offset = dataObjects.gmtOffset;
			int timestamp = dataObjects.timestamp - offset;
			int timestampWithoutOffset = dataObjects.timestamp;

			DateTime resultDateTime = UnixTimeStampToDateTime(timestamp);
			DateTime resultDateTimeWithoutOffset = UnixTimeStampToDateTime(timestampWithoutOffset);
			DateTime currentSystemDateTime = SystemTime.GetSystemTime();

			Console.WriteLine("New datetime with offset: \t{0}", resultDateTime.ToString(outputPattern));
			Console.WriteLine("New datetime without offset: \t{0}", resultDateTimeWithoutOffset.ToString(outputPattern));
			Console.WriteLine("Current system datetime: \t{0}", currentSystemDateTime.ToString(outputPattern));

			if (autoConfirm)
			{
				isConfirmed = true;
			}
			else
			{
				Console.Write("Confirm the system change (y/n): ");
				string confirm = Console.ReadLine();
				isConfirmed = confirm == "y";
			}

			if (isConfirmed)
			{
				// TA-DA
				SystemTime.ChangeSystemDateTime(resultDateTime);
			}
		}

		public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp); //.ToLocalTime();
			return dtDateTime;
		}
	}
}