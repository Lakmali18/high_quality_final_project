using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GetFeedback : Categories
    {
        public GetFeedback() { }

        public GetFeedback(int studentId, string courseName, int duration, string sinNo) : base(studentId, courseName, duration, sinNo)
        {

        }

        public override void Connect()
        {
            Console.WriteLine("Provided feedback");
        }

        public override void SinNoInfo()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("Get feedback {0}", base.ToString());
        }
    }
}
