using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebApp.Tests
{
    public class Plans : IEnumerable<Plan>
    {
        private readonly List<Plan> _plans;

        public Plans()
        {
            _plans = new List<Plan>();
        }

        public void Add(Plan plan)
        {
            _plans.Add(plan);
        }

        public IEnumerator<Plan> GetEnumerator()
        {
            return _plans.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
