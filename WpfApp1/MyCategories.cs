using System;
using ConsoleApp1;

namespace WpfApp1
{
    public class MyCategories : Categories
    {
        string categoryType;
        int intType;
        Categories innerCategories;

        public string CategoryType { get => categoryType; set => categoryType = value; }
        public Categories InnerCategories { get => innerCategories; set => innerCategories = value; }
        public int IntType { get => intType; set => intType = value; }

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
