using System;

namespace DemoWebApp.Tests
{
    public class Plan
    {
        public string PackageName { get; set; }
        public string Province { get; set; }
        public string Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public Plan(string province, string packageName, string price)
        {            
            Province = province;
            PackageName = packageName;
            Price = price;
            CreatedAt = DateTime.Now;
        }
    }
}
