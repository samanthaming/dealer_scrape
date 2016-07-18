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

        public void AddPlan(string province, string packageName, string price)
        {
            PlanList.Add(new Plan(province, packageName, price));
        }
    }
}
