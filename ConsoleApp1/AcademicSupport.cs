using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AcademicSupport : Categories
    {
        public AcademicSupport() { }

        public AcademicSupport(int studentId, string profName, string courseName, int duration, string sinNo) : base(studentId, profName, courseName, duration, sinNo)
        {

        }

        public override void Connect()
        {
            Console.WriteLine("Provided academic support");
        }

        public override void SinNoInfo()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return string.Format("Academic support {0}", base.ToString());
        }
    }
}
