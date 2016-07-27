using System;
using System.Dynamic;

namespace DemoWebApp.Tests
{
    public class Plan
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public string Price { get; set; }
        public string Term { get; set; }
        public string Tab { get; set; }
        public string Calling { get; set; }
        public string Id { get; set; }
        public string Province { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsNew { get; set; }

        public Plan()
        {
            IsNew = false;
            IsProcessed = false;
            CreatedAt = DateTime.Now;
        }
    }
}
