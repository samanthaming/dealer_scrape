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

        public Plan(
            string name,
            string data,
            string price,
            string term,
            string tab,
            string calling,
            string id,
            string province)
        {
            Name = name;
            Data = data;
            Price = price;
            Term = term;
            Tab = tab;
            Province = province;
            Calling = calling;
            Id = id;
            CreatedAt = DateTime.Now;
            IsProcessed = false;
            IsNew = false;
        }
    }
}
