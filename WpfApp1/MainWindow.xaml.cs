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
        string[] customerType = null;

        public ObservableCollection<MyCategories> DisplayCategories { get => displayCategories; set => displayCategories = value; }
        public MyCategories MyCategory { get => myCategory; set => myCategory = value; }
        public string[] CustomerType { get => customerType; set => customerType = value; }
        public MainWindow()
        {
            InitializeComponent();
            customerType = Enum.GetNames(typeof(CategoryTypes));
            DisplayCategories = new ObservableCollection<MyCategories>();
            DataContext = this;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression beAge = BindingOperations.GetBindingExpression(txtStudentId, TextBox.TextProperty);
            BindingExpression beHouseSize = BindingOperations.GetBindingExpression(txtCourseName, TextBox.TextProperty);
            BindingExpression bePaddockSize = BindingOperations.GetBindingExpression(txtDuration, TextBox.TextProperty);
            BindingExpression beCreditCardNo = BindingOperations.GetBindingExpression(txtSinNo, TextBox.TextProperty);

            if (beAge.HasError || beHouseSize.HasError || bePaddockSize.HasError || beCreditCardNo.HasError)
            {
                return;
            }

            bool result = true;
            bool isValidStudentId = int.TryParse(txtStudentId.Text, out int studentId);
            string isValideCourseName = txtCourseName.Text;
            bool isValidDuration = int.TryParse(txtDuration.Text, out int duration);
            string obscuredSinNo = SinNumberHelper.ObscureCreditCardNumber(txtSinNo.Text);

            MyCategories myProperties = new MyCategories();
            myProperties.StudentId = studentId;
            myProperties.CourseName = isValideCourseName;
            myProperties.Duration = duration;
            myProperties.SinNo = obscuredSinNo;
            myProperties.Type = (string)cmbCategoryType.SelectedValue;

            if (result && isValidStudentId && isValidDuration)
            {
                Categories properties = null;
                switch (cmbCategoryType.SelectedIndex)
                {
                    case 0:
                        properties = new AcademicSupport(studentId, isValideCourseName, duration, obscuredSinNo);
                        break;
                    case 1:
                        properties = new CareerAdvices(studentId, isValideCourseName, duration, obscuredSinNo);
                        break;
                    case 2:
                        properties = new GetFeedback(studentId, isValideCourseName, duration, obscuredSinNo);
                        break;
                    default:
                        MessageBox.Show("Program Error");
                        return;
                }
                myProperties.InnerCategories = properties;
                DisplayCategories.Add(myProperties);
                cmbCategoryType.SelectedIndex = -1;
                txtStudentId.Text = string.Empty;
                txtCourseName.Text = string.Empty;
                txtDuration.Text = string.Empty;
                txtSinNo.Text = string.Empty;
            }
        }

        private void txtCourseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  txtCreditCard.Foreground = Brushes.Black;
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
                Categories properties = propertyList[i];
                MyCategories newProperties = new MyCategories();
                newProperties.StudentId = properties.StudentId;
                newProperties.CourseName = properties.CourseName;
                newProperties.Duration = properties.Duration;
                newProperties.SinNo = properties.SinNo;

                string[] arrStr = properties.GetType().ToString().Split('.');
                string fullType = arrStr[arrStr.Length - 1];
                newProperties.Type = fullType.Substring(0, fullType.Length);
                DisplayCategories.Add(newProperties);
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
            TextWriter tw = new StreamWriter("customer_info.xml");
            serializer.Serialize(tw, propertyList);
            tw.Close();
        }
        private void ReadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryList));
            TextReader tr = new StreamReader("customer_info.xml");
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

    }
}
