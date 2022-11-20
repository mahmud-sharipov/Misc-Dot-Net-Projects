using ClassJoural.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassJoural
{
    public partial class FirstRaning : Window
    {
       public Setting setting;
        DataProvider dataProvider;
        ObservableCollection<Student> students;
        ObservableCollection<Parent> parents=new ObservableCollection<Parent>();
        ObservableCollection<Subject> subjects = new ObservableCollection<Subject>();
        ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();
        ObservableCollection<Schedule> schedules = new ObservableCollection<Schedule>();

        int list = -1;
        public FirstRaning(DataProvider dp)
        {
            dataProvider = dp;
            setting = dataProvider.GetEntities<Setting>().First();
            InitializeComponent();

            ClassNumber.ItemsSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            Content1.DataContext = setting;
            Parents.ItemsSource = parents;
            StudentList.ItemsSource = students;
            SubjectTeacher.ItemsSource = teachers;
            SubjectList.ItemsSource = subjects;
            ScheduleList.ItemsSource = schedules;
            teachers.Add(setting.ClassTeacher);
        }

        private void FinishBtn_Click(object sender, RoutedEventArgs e)
        {
            setting.FirsrRun = false; ;
            dataProvider.SaveChanges();
            this.Close();
        }

        private void NextBtn1_Click(object sender, RoutedEventArgs e)
        {
            Content1.Visibility = Visibility.Hidden;
            Content2.Visibility = Visibility.Visible;
            Header.Text = "Хонандагони синф";
            if (students == null)
            {
                students = new ObservableCollection<Student>();
                for (int i = 0; i < setting.ClassStudentCount; i++)
                {
                    students.Add(dataProvider.Add<Student>());
                }
                StudentList.ItemsSource = students;
            }
            else
            {
                if (setting.ClassStudentCount > students.Count)
                    for (int i = 0; i < setting.ClassStudentCount - students.Count; i++)
                        students.Add(dataProvider.Add<Student>());
                else if (setting.ClassStudentCount < students.Count)
                    for (int i = students.Count; i > setting.ClassStudentCount; i--)
                    {
                        dataProvider.Delete(students[i - 1]);
                        students.RemoveAt(i - 1);
                    }
            }
        }

        private void NextBtn2_Click(object sender, RoutedEventArgs e)
        {
            Content2.Visibility = Visibility.Hidden;
            Content3.Visibility = Visibility.Visible;
            Header.Text = "Фанҳо";
            if (subjects == null)
            {
                for (int i = 0; i < setting.ClassSubjectCount; i++)
                {
                    subjects.Add(dataProvider.Add<Subject>());
                }
                SubjectList.ItemsSource = subjects;
            }
            else
            {
                if (setting.ClassSubjectCount > subjects.Count)
                {
                    var r = subjects.Count;
                    for (int i = 0; i < setting.ClassSubjectCount - r; i++)
                        subjects.Add(dataProvider.Add<Subject>());
                }
                else if (setting.ClassSubjectCount < subjects.Count)
                    for (int i = subjects.Count; i > setting.ClassSubjectCount; i--)
                    {
                        dataProvider.Delete(subjects[i - 1]);
                        subjects.RemoveAt(i - 1);
                    }
            }
        }

        private void BackBtn2_Click(object sender, RoutedEventArgs e)
        {
            Content2.Visibility = Visibility.Hidden;
            Content1.Visibility = Visibility.Visible;
            Header.Text = "Маълумоти умӯмӣ";
        }

        private void NextBtn3_Click(object sender, RoutedEventArgs e)
        {
            Content3.Visibility = Visibility.Hidden;
            Content4.Visibility = Visibility.Visible;
            Header.Text = "Ҷадвали дарсӣ";
            foreach (var item in subjects)
            {
                if (item.Schedule == null)
                {
                    var newSchedule = dataProvider.Add<Schedule>();
                    schedules.Add(newSchedule);
                    newSchedule.Subject = item;
                    item.Schedule = newSchedule;
                }
            }
            ScheduleList.ItemsSource = schedules;
        }

        private void BackBtn3_Click(object sender, RoutedEventArgs e)
        {
            Content3.Visibility = Visibility.Hidden;
            Content2.Visibility = Visibility.Visible;
            Header.Text = "Хонандагони синф";
        }

        private void BackBtn4_Click(object sender, RoutedEventArgs e)
        {
            Content4.Visibility = Visibility.Hidden;
            Content3.Visibility = Visibility.Visible;
            Header.Text = "Фанҳо";
        }

        private void AddNewParentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewParantPhone.Text != "" & NewParantFIO.Text != "")
            {
                var newParent = dataProvider.Add<Parent>();
                newParent.FIO = NewParantFIO.Text; newParent.Phone = NewParantPhone.Text;
                parents.Add(newParent);
                NewParantPhone.Text = "";
                NewParantFIO.Text = "";
            }
            else
                MessageBox.Show("Маълумотҳоро ба пурраги дохил кунед!");
        }

        private void AddParentForStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Parents.SelectedIndex != -1 && StudentList.SelectedIndex != -1 && !(StudentList.SelectedItem as Student).Parents.Contains(Parents.SelectedItem as Parent))
            {
                (StudentList.SelectedItem as Student).Parents.Add(Parents.SelectedItem as Parent);
            }
        }

        private void DeleteParentFromStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StudentParents.SelectedIndex != -1)
            {
                (StudentList.SelectedItem as Student).Parents.Remove(StudentParents.SelectedItem as Parent);
            }
        }

        private void AddNewTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewTeacherFIO.Text != "")
            {
                teachers.Add(new Teacher() { FIO = NewTeacherFIO.Text });
                NewTeacherFIO.Text = "";
            }
            else
                MessageBox.Show("Маълумотҳоро ба пурраги дохил кунед!");
        }

        private void DeleteTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectTeacher.SelectedIndex != -1)
            {
                if ((SubjectTeacher.SelectedItem as Teacher).Id == setting.ClassTeacher.Id)
                {
                    MessageBox.Show("Муаллими мазкур роҳбарӣ синф аст! \n Хориҷкунии маълумотҳои роҳбари синф ғайри имкон аст.");
                    return;
                }
                dataProvider.Delete(SubjectTeacher.SelectedItem as Teacher);
                teachers.Remove(SubjectTeacher.SelectedItem as Teacher);
            }
        }
    }
}
