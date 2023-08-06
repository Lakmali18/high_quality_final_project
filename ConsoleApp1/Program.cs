using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        public enum CategoryTypes
        {
            AcademicSupport = 1,
            CareerAdvices = 2,
            GetFeedback = 3
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Go();

        }

        public void Go()
        {

            using (CategoryList nl = new CategoryList())
            {
                AcademicSupport blt = new AcademicSupport(8906745, "High Quality Software Programming",15, "954879067");
                nl.Add(blt);
                CareerAdvices et = new CareerAdvices(8909845, "System Analysis and Design", 30, "959674589");
                nl.Add(et);
                GetFeedback ft = new GetFeedback(8900375, "Software Testing Tools", 20, "980896578");
                nl.Add(ft);
                nl.Sort();
                for (int i = 0; i < nl.Count(); i++)
                {
                    Console.WriteLine(nl[i]);
                    nl[i].CategoryDel();
                }
            }

        }
    }
}
