using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public enum CategoryTypes { AcademicSupport, CareerAdvices, GetFeedback }

    [XmlRoot("CategoryList")]
    [XmlInclude(typeof(AcademicSupport))]
    [XmlInclude(typeof(CareerAdvices))]
    [XmlInclude(typeof(GetFeedback))]

    internal class CategoryList : IDisposable
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
