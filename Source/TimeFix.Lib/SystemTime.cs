using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TimeFix.Lib
{
	/// <summary>
	/// http://stackoverflow.com/a/650872/84852
	/// </summary>

	[StructLayout(LayoutKind.Sequential)]
	public struct SYSTEMTIME
	{
		public ushort wYear;
		public ushort wMonth;
		public ushort wDayOfWeek;
		public ushort wDay;
		public ushort wHour;
		public ushort wMinute;
		public ushort wSecond;
		public ushort wMilliseconds;
	}

	public class SystemTime
	{
		[DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
		public static extern bool SetSystemTime(ref SYSTEMTIME st);

		[DllImport("kernel32.dll", EntryPoint = "GetSystemTime", SetLastError = true)]
		public extern static void GetSystemTime(ref SYSTEMTIME sysTime);

		public static DateTime GetSystemTime()
		{
			SYSTEMTIME sysTime = new SYSTEMTIME();

			GetSystemTime(ref sysTime);

			DateTime dateTime = new DateTime(sysTime.wYear, sysTime.wMonth, sysTime.wDay, sysTime.wHour, sysTime.wMinute, sysTime.wSecond);

			return dateTime;
		}

		public static void ChangeSystemDateTime(DateTime dateTime)
		{
			SYSTEMTIME st = new SYSTEMTIME();
			st.wYear = (ushort)dateTime.Year; // must be short
			st.wMonth = (ushort)dateTime.Month;
			st.wDay = (ushort)dateTime.Day;
			st.wHour = (ushort)dateTime.Hour;
			st.wMinute = (ushort)dateTime.Minute;
			st.wSecond = (ushort)dateTime.Second;

			SetSystemTime(ref st); // invoke this method.
		}
	}
}