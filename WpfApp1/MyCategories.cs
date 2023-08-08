using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace WpfApp1
{
    public class MyCategories : Categories
    {
        string type;
        int intType;
        int intProfType;
        Categories innerCategories;

        public string Type { get => type; set => type = value; }
        public Categories InnerCategories { get => innerCategories; set => innerCategories = value; }
        public int IntType { get => intType; set => intType = value; }

        public int IntProfType { get => intProfType; set => intProfType = value; }

        public override void Connect()
        {
            throw new NotImplementedException();
        }

        public override void SinNoInfo()
        {
            throw new NotImplementedException();
        }
    }
}
