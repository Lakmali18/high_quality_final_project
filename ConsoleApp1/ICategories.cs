using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public delegate void CategoryDelegate();
    public interface ICategories : IComparable<ICategories>
    {
        int StudentId { get; set; }
        string CourseName { get; set; }
        int Duration { get; set; }
        string SinNo { get; set; }
        CategoryDelegate CategoryDel { get; set; }
        void Connect();
        void ConfirmAvailibilty();
        void ArrangeConnection();
    }
}
