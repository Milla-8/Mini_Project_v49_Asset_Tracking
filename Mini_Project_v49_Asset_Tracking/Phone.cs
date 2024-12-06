using System;
namespace Mini_Project_v49_Asset_Tracking
{
    public class Phone : Asset
    {
        public string Brand { get; set; }
        public string Model { get; set; }

        public Phone(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }
    }
}

