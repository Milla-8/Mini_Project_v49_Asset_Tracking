using System;
namespace Mini_Project_v49_Asset_Tracking
{
	public class Office
	{
		public string Country { get; set; }
        public string Currency { get; set; }

        public Office(string country, string currency)
		{
			Country = country;
			Currency = currency;
		}
	}
}

