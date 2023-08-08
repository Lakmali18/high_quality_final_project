using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public enum CategoryTypes { Academic_Support, Career_Advices, Get_Feedback }
    public enum ProfessorTypes { Igor_Pustylnick, Daljit_Kaur, Ruchika_Ruchika, Sachin_Parikh }

    [XmlRoot("CategoryList")]
    [XmlInclude(typeof(AcademicSupport))]
    [XmlInclude(typeof(CareerAdvices))]
    [XmlInclude(typeof(GetFeedback))]

    public class CategoryList : IDisposable
    {
        private List<Categories> categoryList = null;

        [XmlArray("Categories")]
        [XmlArrayItem("Category", typeof(Categories))]


        public List<Categories> ListCategory { get => categoryList; set => categoryList = value; }

        public CategoryList()
        {
            ListCategory = new List<Categories>();
        }

        public void Add(Categories property)
        {
            ListCategory.Add(property);
        }


        public int Count()
        {
            return ListCategory.Count();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Sort()
        {
            ListCategory.Sort();
        }

        public void Clear()
        {
            ListCategory.Clear();
        }

        public Categories this[int i]
        {
            get { return ListCategory[i]; }
        }
    }
}
