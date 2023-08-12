using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public abstract class Categories : ICategories, INotifyPropertyChanged
    {
        private int studentId;
        private string profName;
        private string courseName;
        private int duration;
        private string sinNo;
        private CategoryDelegate catDel = null;


        private string time;
        private bool isEditing = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public Categories()
        {
            SetUpDelegate();

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Categories(int studentId, string profName, string courseName, int duration, string sinNo)
        {
            this.studentId = studentId;
            this.profName = profName;
            this.courseName = courseName;
            this. duration = duration;
            this.sinNo = sinNo;
            SetUpDelegate();

        }

        public bool IsEditing {
            get { return isEditing; }
            set 
            { 
                if (isEditing != value)
                {
                    isEditing = value;
                    OnPropertyChanged(nameof(IsEditing));
                }
            }
        }
        public int StudentId { get => studentId; set => studentId = value; }
        public string ProfName { get => profName; set => profName = value; }
        public string CourseName { get => courseName; set => courseName = value; }
        public int Duration { get => duration; set => duration = value; }
        public string SinNo { get => sinNo; set => sinNo = value; }
        [XmlIgnore]

        public CategoryDelegate CategoryDel { get => catDel; set => catDel = value; }
        public string ViewSinNo { get => ConcealedSinNo(); }

        public string Time { get => time; set => time = value; }

        public void ConfirmAvailibilty()
        {
            Console.WriteLine("Professor availibility confirmed");
        }

        public abstract void Connect();

        public abstract void SinNoInfo();

        public void ArrangeConnection()
        {
            Console.WriteLine("Professor has connected");
        }

        public bool CheckSinNo()
        {
            bool result = false;
            if (SinNo != null && SinNo != string.Empty)
            {
                char[] passArray = SinNo.ToCharArray();
                if (passArray.Length == 16)
                {
                    for (int i = 0; i < passArray.Length; i++)
                    {
                        if (char.IsDigit(passArray[i]))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }


                }
            }
            return result;
        }

        public string ConcealedSinNo()
        {

            char[] passArray = SinNo.ToCharArray();
            {

                    for (int i = 1; i < 6; i++)
                    {
                        passArray[i] = 'X';
                    }
                
            }
            return new string(passArray);
        }


        public int CompareTo(ICategories other)
        {
            return studentId.CompareTo(other.StudentId);
        }

        public override string ToString()
        {

            return string.Format("The student ID is {0}. {1} mins conversation will hold in the {2} course professor. \nThe recored SIN number is {3}.", studentId, duration, courseName, ViewSinNo);
        }

        private void SetUpDelegate()
        {
            catDel += ConfirmAvailibilty;
            catDel += ArrangeConnection;
            catDel += Connect;

        }
    }
}
