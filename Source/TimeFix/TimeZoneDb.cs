using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

using System.Net.Http.Headers;

namespace TimeFix
{
	public class TimeZoneDb
	{
		public DataObject GetData()
		{
			string baseUrl = "http://api.timezonedb.com/";
			string urlPattern = "?zone={0}&key={1}&format=json";

			string timezone = ConfigurationManager.AppSettings["timezone"];
			string apiKey = ConfigurationManager.AppSettings["apiKey"];

			string urlParameters = string.Format(urlPattern, timezone, apiKey);

			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(baseUrl);

			// Add an Accept header for JSON format.
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));

			// List data response.
			HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!

			if (response.IsSuccessStatusCode)
			{
				// Parse the response body. Blocking!
				DataObject dataObjects = response.Content.ReadAsAsync<DataObject>().Result;
				return dataObjects;
			}
			else
			{
				Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
			}

			return null;
		}
	}
}