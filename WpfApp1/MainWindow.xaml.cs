using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes; 
using System.Xml.Serialization;
using ConsoleApp1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<MyCategories> displayCategories = null;
        CategoryList propertyList = new CategoryList();
        MyCategories myCategory = new MyCategories();
        string[] categoryType = null;
        string[] professorType = null;
        string selectedProf = string.Empty;

        public ObservableCollection<MyCategories> DisplayCategories { get => displayCategories; set => displayCategories = value; }
        public MyCategories MyCategory { get => myCategory; set => myCategory = value; }
        public string[] CategoryType { get => categoryType; set => categoryType = value; }
        public string[] ProfessorType { get => professorType; set => professorType = value; }

        public MainWindow()
        {
            InitializeComponent();
            CategoryType = Enum.GetNames(typeof(CategoryTypes));
            ProfessorType = Enum.GetNames(typeof(ProfessorTypes));
            DisplayCategories = new ObservableCollection<MyCategories>();
            DataContext = this;
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression beStudentId = BindingOperations.GetBindingExpression(txtStudentId, TextBox.TextProperty);
            BindingExpression beCourseName = BindingOperations.GetBindingExpression(txtCourseName, TextBox.TextProperty);
            BindingExpression beDuration = BindingOperations.GetBindingExpression(txtDuration, TextBox.TextProperty);
            BindingExpression beSinNo = BindingOperations.GetBindingExpression(txtSinNo, TextBox.TextProperty);

            if (beStudentId.HasError || beCourseName.HasError || beDuration.HasError || beSinNo.HasError)
            {
                return;
            }

            bool result = true;
            bool isValidStudentId = int.TryParse(txtStudentId.Text, out int studentId);
            string isValideCourseName = txtCourseName.Text;
            bool isValidDuration = int.TryParse(txtDuration.Text, out int duration);
            string obscuredSinNo = SinNumberHelper.ObscureCreditCardNumber(txtSinNo.Text);


            MyCategories myCategories = new MyCategories();
            myCategories.StudentId = studentId;
            myCategories.CourseName = isValideCourseName;
            myCategories.Duration = duration;
            myCategories.SinNo = obscuredSinNo;
            myCategories.CategoryType = (string)cmbCategoryType.SelectedValue;
            myCategories.ProfType = (string)cmbProfessorName.SelectedValue;

            if (result && isValidStudentId && isValidDuration)
            {
                Categories categories = null;
                switch (cmbCategoryType.SelectedIndex)
                {
                    case 0:
                        categories = new AcademicSupport(studentId, isValideCourseName, duration, obscuredSinNo);
                        break;
                    case 1:
                        categories = new CareerAdvices(studentId, isValideCourseName, duration, obscuredSinNo);
                        break;
                    case 2:
                        categories = new GetFeedback(studentId, isValideCourseName, duration, obscuredSinNo);
                        break;
                    default:
                        MessageBox.Show("Program Error");
                        return;
                }
                myCategories.InnerCategories = categories;
                DisplayCategories.Add(myCategories);
                cmbCategoryType.SelectedIndex = -1;
                txtStudentId.Text = string.Empty;
                txtCourseName.Text = string.Empty;
                txtDuration.Text = string.Empty;
                txtSinNo.Text = string.Empty;
            }
        }

        private void txtStudentId_TextChanged(object sender, TextChangedEventArgs e)
        {
             txtStudentId.Foreground = Brushes.Black;
        }
        private void txtCourseName_TextChanged(object sender, TextChangedEventArgs e)
        {
              txtCourseName.Foreground = Brushes.Black;
        }

        private void txtSinNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSinNo.Foreground = Brushes.Black;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            propertyList.Clear();
            foreach (MyCategories props in DisplayCategories)
            {
                propertyList.Add(props.InnerCategories);
            }
            WriteToFile();
        }

        private void BtnDisplay_Click(object sender, RoutedEventArgs e)
        {
            propertyList.Clear();
            ReadFromFile();
            DisplayCategories.Clear();
            for (int i = 0; i < propertyList.Count(); i++)
            {
                Categories categories = propertyList[i];
                MyCategories newCategories = new MyCategories();
                newCategories.StudentId = categories.StudentId;
                newCategories.CourseName = categories.CourseName;
                newCategories.Duration = categories.Duration;
                newCategories.SinNo = categories.SinNo;

                string[] arrStr = categories.GetType().ToString().Split('.');
                string fullType = arrStr[arrStr.Length - 1];
                newCategories.CategoryType = fullType.Substring(0, fullType.Length);
                DisplayCategories.Add(newCategories);

              
                newCategories.ProfType = fullType.Substring(0, fullType.Length);
                DisplayCategories.Add(newCategories);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }


        private void WriteToFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryList));
            TextWriter tw = new StreamWriter("profconect_info.xml");
            serializer.Serialize(tw, propertyList);
            tw.Close();
        }
        private void ReadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryList));
            TextReader tr = new StreamReader("profconect_info.xml");
            propertyList = (CategoryList)serializer.Deserialize(tr);
            tr.Close();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var query = from properties in DisplayCategories
                        where properties.StudentId.ToString().ToLower() == txtSearch.Text.ToLower().Trim()
                        select properties;
            MyGrid.ItemsSource = query;
        }

        private void ProfName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProfessorName.SelectedItem != null)
            {
                //var profTypes = (ProfessorTypes)cmbProfessorName.SelectedItem;
                //selectedProf = Enum.GetName(typeof(ProfessorTypes), profTypes);

              
            }
        }
    }
}
