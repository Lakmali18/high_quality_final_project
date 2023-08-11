using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
        CategoryList categoryList = new CategoryList();
        MyCategories myCategory = new MyCategories();
        string[] categoryType = null;

        public List<string> ProfessorTypes = new List<string>
            { "Igor_Pustylnick", "Daljit_Kaur", "Ruchika_Ruchika", "Sachin_Parikh"};

        public ObservableCollection<MyCategories> DisplayCategories { get => displayCategories; set => displayCategories = value; }
        public MyCategories MyCategory { get => myCategory; set => myCategory = value; }
        public string[] CategoryType { get => categoryType; set => categoryType = value; }

        public string SelectedCustomerType { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Go();
            Console.WriteLine("GUI running");
           
        }

        public void Go()
        {
            CategoryType = Enum.GetNames(typeof(CategoryTypes));
            DisplayCategories = new ObservableCollection<MyCategories>();
            cmbProfessorName.ItemsSource = ProfessorTypes;
            //object ActionButtons = 'e';
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
            string isValidProfName = cmbProfessorName.Text;
            string isValideCourseName = txtCourseName.Text;
            bool isValidDuration = int.TryParse(txtDuration.Text, out int duration);
            string obscuredSinNo = SinNumberHelper.ObscureSINNumber(txtSinNo.Text);


            MyCategories myCategories = new MyCategories();
            myCategories.StudentId = studentId;
            myCategories.CourseName = isValideCourseName;
            myCategories.Duration = duration;
            myCategories.SinNo = obscuredSinNo;
            myCategories.CategoryType = (string)cmbCategoryType.SelectedValue;
            myCategories.ProfName = (string)cmbProfessorName.SelectedValue;

            if (result && isValidStudentId && isValidDuration)
            {
                Categories categories = null;
                switch (cmbCategoryType.SelectedIndex)
                {
                    case 0:
                        categories = new AcademicSupport(studentId, isValidProfName, isValideCourseName, duration, obscuredSinNo);
                        break;
                    case 1:
                        categories = new CareerAdvices(studentId, isValidProfName, isValideCourseName, duration, obscuredSinNo);
                        break;
                    case 2:
                        categories = new GetFeedback(studentId, isValidProfName, isValideCourseName, duration, obscuredSinNo);
                        break;
                    default:
                        MessageBox.Show("Program Error");
                        return;
                }

                myCategories.InnerCategories = categories;
                DisplayCategories.Add(myCategories);

                cmbCategoryType.SelectedIndex = -1;
                cmbProfessorName.SelectedIndex = -1;
                txtStudentId.Text = string.Empty;
                txtCourseName.Text = string.Empty;
                txtDuration.Text = string.Empty;
                txtSinNo.Text = string.Empty;
            }
        }

        private void BtnDisplay_Click(object sender, RoutedEventArgs e)
        {
            categoryList.Clear();
            ReadFromFile();
            DisplayCategories.Clear();
            for (int i = 0; i < categoryList.Count(); i++)
            {
           
                Categories categories = categoryList[i];
                MyCategories newCategories = new MyCategories();

                if (categories != null)
                {
                    newCategories.StudentId = categories.StudentId;
                    newCategories.ProfName = categories.ProfName;
                    newCategories.CourseName = categories.CourseName;
                    newCategories.Duration = categories.Duration;
                    newCategories.SinNo = categories.SinNo;

                    string[] arrStr = categories.GetType().ToString().Split('.');
                    string fullType = arrStr[arrStr.Length - 1];
                    newCategories.CategoryType = fullType.Substring(0, fullType.Length);
                    DisplayCategories.Add(newCategories);
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            categoryList.Clear();
            foreach (MyCategories props in DisplayCategories)
            {
                categoryList.Add(props.InnerCategories);
            }
            WriteToFile();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var query = from properties in DisplayCategories
                        where properties.StudentId.ToString().ToLower() == txtSearch.Text.ToLower().Trim()
                        select properties;
            MyGrid.ItemsSource = query;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int studentId)
            {
                
                // Handle button click using the itemId
                Console.WriteLine($"Editing student with ID: {studentId}");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button && button.Tag is MyCategories item)
            {
                //YourDataSource.Remove(item);
                Console.WriteLine($"Deleting student with ID: {item}");
                DisplayCategories.Remove(item);
                BtnSave_Click(sender, e);
                BtnDisplay_Click(sender, e);
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

        private void WriteToFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryList));
            TextWriter tw = new StreamWriter("profconect_info.xml");
            serializer.Serialize(tw, categoryList);
            tw.Close();
        }
        private void ReadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryList));
            TextReader tr = new StreamReader("profconect_info.xml");
            categoryList = (CategoryList)serializer.Deserialize(tr);
            tr.Close();
        }

        private void comboBox_Loaded(object sender, RoutedEventArgs e)
        {
            cmbProfessorName.SelectedIndex = 0;
        }
    }
}
