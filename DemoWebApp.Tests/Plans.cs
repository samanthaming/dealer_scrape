using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebApp.Tests
{
    public class Plans
    {
       public List<Plan> PlanList { get; set; }

        public Plans()
        {
            PlanList = new List<Plan>();
        }

        public void AddPlan(string name, string data, string price, string term, string tab, string calling, string id)
        {
            PlanList.Add(new Plan(name, data, price, term, tab, calling, id));
        }
    }
}
