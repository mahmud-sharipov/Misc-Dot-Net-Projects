using ClassJoural.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClassJoural
{
    public class AppViewModel : INotifyPropertyChanged
    {
        public DataProvider dataProvider;
        WebBrowser webPage;
        string curDir = Directory.GetCurrentDirectory();
        int currentQuarter = 1;
        Quarter quarter = new Quarter();
        public Action RefreshWebPage;

        public AppViewModel(WebBrowser webBwpwser)
        {
            try
            {
                quarter.Load(curDir + @"\quarter.txt");
                DataProvider = new DataProvider();
                Students = new ObservableCollection<Student>(DataProvider.GetEntities<Student>().OrderBy(e => e.LastName).ToList());
                Subjects = new ObservableCollection<Subject>(DataProvider.GetEntities<Subject>().OrderBy(e => e.Name).ToList());
                Parents = new ObservableCollection<Parent>(DataProvider.GetEntities<Parent>().OrderBy(e => e.FIO));
                CurrentSubject = Subjects[0];
                webPage = webBwpwser;
                RefreshWebView(quarter.Quarter1, quarter.Quarter2);
                Title = string.Format("{0}: марҳилаи {1}", CurrentSubject.Name, currentQuarter);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            new Task(InitDateTime).Start();
        }

        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set { students = value; RaisePropertyChanged(nameof(Students)); }
        }

        private ObservableCollection<Parent> parents;
        public ObservableCollection<Parent> Parents
        {
            get { return parents; }
            set { parents = value; RaisePropertyChanged(nameof(Parents)); }
        }

        private ObservableCollection<Subject> subjects;
        public ObservableCollection<Subject> Subjects
        {
            get { return subjects; }
            set { subjects = value; RaisePropertyChanged(nameof(Subjects)); }
        }

        private Subject currentSubject;
        public Subject CurrentSubject
        {
            get { return currentSubject; }
            set
            {
                currentSubject = value;
                RaisePropertyChanged(nameof(CurrentSubject));
                currentQuarter = 1;
                Title = string.Format("{0}: марҳилаи {1}", CurrentSubject.Name, currentQuarter);
                RefreshWebView(quarter.Quarter1, quarter.Quarter2);
            }
        }

        public DataProvider DataProvider
        {
            get { return dataProvider; }
            set { dataProvider = value; RaisePropertyChanged(nameof(DataProvider)); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(nameof(Title)); }
        }

        private string currentDateTime;
        public string CurrentDateTime
        {
            get { return currentDateTime; }
            set { currentDateTime = value; RaisePropertyChanged(nameof(CurrentDateTime)); }
        }

        private Visibility journalV = Visibility.Visible;
        public Visibility JournalV
        {
            get { return journalV; }
            set { journalV = value; RaisePropertyChanged(nameof(JournalV)); }
        }

        private Visibility resultV = Visibility.Collapsed;
        public Visibility ResultV
        {
            get { return resultV; }
            set { resultV = value; RaisePropertyChanged(nameof(ResultV)); }
        }

        private ObservableCollection<StadyResult> studentResult = new ObservableCollection<StadyResult>();

        public ObservableCollection<StadyResult> StudentResult
        {
            get
            {
                studentResult.Clear();
                try
                {
                    foreach (var item in Students.OrderBy(e => e.FullName))
                        studentResult.Add(new StadyResult(CurrentSubject, item, quarter));
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                return studentResult;
            }
        }

        private ObservableCollection<StudentResultStaty> studentStaduRes = new ObservableCollection<StudentResultStaty>();

        public ObservableCollection<StudentResultStaty> StudentStaduReS
        {
            get
            {
                studentStaduRes.Clear();
                try
                {
                    foreach (var item in Students.OrderBy(e => e.FullName))
                        studentStaduRes.Add(new StudentResultStaty(item, Subjects.ToList()));
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                return studentStaduRes;
            }
        }

        #region Command

        private RelayCommand quarter1;
        public RelayCommand Quarter1
        {
            get
            {
                if (quarter1 == null)
                    quarter1 = new RelayCommand(param =>
                    {
                        try
                        {
                            currentQuarter = 1;
                            RefreshWebView(quarter.Quarter1, quarter.Quarter2);
                            Title = string.Format("{0}: марҳилаи {1}", CurrentSubject.Name, currentQuarter);
                            ResultV = Visibility.Collapsed;
                            JournalV = Visibility.Visible;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return quarter1;
            }
        }

        private RelayCommand quarter2;
        public RelayCommand Quarter2
        {
            get
            {
                if (quarter2 == null)
                    quarter2 = new RelayCommand(param =>
                    {
                        try
                        {
                            currentQuarter = 2;
                            RefreshWebView(quarter.Quarter2, quarter.Quarter3);
                            Title = string.Format("{0}: марҳилаи {1}", CurrentSubject.Name, currentQuarter);
                            ResultV = Visibility.Collapsed;
                            JournalV = Visibility.Visible;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }

                    });
                return quarter2;
            }
        }

        private RelayCommand quarter3;
        public RelayCommand Quarter3
        {
            get
            {
                if (quarter3 == null)
                    quarter3 = new RelayCommand(param =>
                    {
                        try
                        {
                            currentQuarter = 3;
                            RefreshWebView(quarter.Quarter3, quarter.Quarter4);
                            Title = string.Format("{0}: марҳилаи {1}", CurrentSubject.Name, currentQuarter);
                            ResultV = Visibility.Collapsed;
                            JournalV = Visibility.Visible;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return quarter3;
            }
        }

        private RelayCommand quarter4;
        public RelayCommand Quarter4
        {
            get
            {
                if (quarter4 == null)
                    quarter4 = new RelayCommand(param =>
                    {
                        try
                        {
                            currentQuarter = 4;
                            RefreshWebView(quarter.Quarter4, quarter.End);
                            Title = string.Format("{0}: марҳилаи {1}", CurrentSubject.Name, currentQuarter);
                            ResultV = Visibility.Collapsed;
                            JournalV = Visibility.Visible;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return quarter4;
            }
        }

        private RelayCommand resultStaty;
        public RelayCommand ResultStaty
        {
            get
            {
                if (resultStaty == null)
                    resultStaty = new RelayCommand(param =>
                    {
                        try
                        {
                            ResultV = Visibility.Visible;
                            JournalV = Visibility.Collapsed;
                            Title = string.Format("{0}: Натиҷаи донишомӯзи", CurrentSubject.Name);
                            RaisePropertyChanged(nameof(StudentResult));
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return resultStaty;
            }
        }

        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                if (save == null)
                    save = new RelayCommand(param =>
                    {
                        try
                        {
                            DataProvider.SaveChanges();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return save;
            }
        }

        private RelayCommand addNewTheme;
        public RelayCommand AddNewTheme
        {
            get
            {
                if (addNewTheme == null)
                    addNewTheme = new RelayCommand(param =>
                    {
                        try
                        {
                            if (CurrentSubject.Schedule.GetDayValue(DateTime.Now.DayOfWeek.ToString()))
                            {
                                if (CurrentSubject.Themes.Where(e => e.Date.Date == DateTime.Now.Date).FirstOrDefault() == null)
                                {
                                    var t = new AddThemeDetail(this);
                                    t.ShowDialog();
                                    if (t.res == true)
                                    {
                                        DataProvider.Add(t.theme);
                                        foreach (var item in t.theme.Details)
                                            DataProvider.Add(item);
                                        t.theme.Subject = CurrentSubject;
                                        CurrentSubject.Themes.Add(t.theme);
                                        //DataProvider.SaveChanges();
                                        RefreshWebView(currentQuarter);
                                    }
                                    else if (t.res == false)
                                    {
                                        //dataProvider.SaveChanges();
                                    }
                                }
                                else
                                    MessageBox.Show("Шумо қайди имрӯзаро сабт кардаед!", "Хатогӣ", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                                MessageBox.Show("Шумо имрӯз дарс надоред!", "Хатогӣ", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return addNewTheme;
            }
        }

        private RelayCommand updataToDayThemDetail;
        public RelayCommand UpdataToDayThemDetail
        {
            get
            {
                if (updataToDayThemDetail == null)
                    updataToDayThemDetail = new RelayCommand(param =>
                    {
                        try
                        {
                            if (CurrentSubject.Schedule.GetDayValue(DateTime.Now.DayOfWeek.ToString()))
                            {
                                if (CurrentSubject.Themes.Where(e => e.Date.Date == DateTime.Now.Date).FirstOrDefault() == null)
                                {
                                    MessageBox.Show("Шумо қайди имрӯзаро сабт накардаед!", "Хатогӣ", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                else
                                {
                                    var t = new UpdateThemeDetail(CurrentSubject.Themes.Where(e => e.Date.Date == DateTime.Now.Date).First());
                                    t.ShowDialog();
                                    //DataProvider.SaveChanges();
                                    RefreshWebView(currentQuarter);
                                }

                            }
                            else
                                MessageBox.Show("Шумо имрӯз дарс надоред!", "Хатогӣ", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return updataToDayThemDetail;
            }
        }

        private RelayCommand themsView;
        public RelayCommand ThemsView
        {
            get
            {
                if (themsView == null)
                    themsView = new RelayCommand(param =>
                    {
                        try
                        {
                            if (currentQuarter == 1)
                                new ThemeView(CurrentSubject, quarter.Quarter1, quarter.Quarter2).ShowDialog();
                            else if (currentQuarter == 2)
                                new ThemeView(CurrentSubject, quarter.Quarter2, quarter.Quarter3).ShowDialog();
                            else if (currentQuarter == 3)
                                new ThemeView(CurrentSubject, quarter.Quarter3, quarter.Quarter4).ShowDialog();
                            else if (currentQuarter == 4)
                                new ThemeView(CurrentSubject, quarter.Quarter4, quarter.End).ShowDialog();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return themsView;
            }
        }

        private RelayCommand thisReset;
        public RelayCommand ThisReset
        {
            get
            {
                if (thisReset == null)
                    thisReset = new RelayCommand(param =>
                    {
                        try
                        {
                            DataProvider.DeleteDb();
                            (param as MainWindow).Close();
                            new Resert().Show();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    });
                return thisReset;
            }
        }

        #endregion

        void InitDateTime()
        {
            while (true)
            {
                Thread.Sleep(1000);
                CurrentDateTime = DateTime.Now.ToString();
            }
        }

        void RefreshWebView(int q)
        {
            if (q == 1) RefreshWebView(quarter.Quarter1, quarter.Quarter2);
            if (q == 2) RefreshWebView(quarter.Quarter2, quarter.Quarter3);
            if (q == 3) RefreshWebView(quarter.Quarter3, quarter.Quarter4);
            if (q == 4) RefreshWebView(quarter.Quarter4, quarter.End);
        }

        void RefreshWebView(DateTime stD, DateTime endD)
        {
            var themes = CurrentSubject.Themes.Where(e => e.Date >= stD & e.Date <= endD).OrderBy(e => e.Date).ToList();
            string[] month = new string[] { "Янв", "Фев", "Мрт", "Апр", "Май ", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек" };
            StreamWriter sw = new StreamWriter(curDir + @"\index.html");
            var css = new StreamReader(curDir + @"\css.css").ReadToEnd();
            sw.WriteLine(@"<html><head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>");
            sw.Write(@"<style>" + css + "</style></head><body>");
            sw.WriteLine(@"<div class='content'><table class='table'  style='width: " + (themes.Count * 50 + 260) + "px'><thead><tr><th id='name'>Ному насаб</th>");
            foreach (var theme in themes)
            {
                sw.WriteLine(@"<th" + (theme.IsExam == true ? " id='thExam'" : "") + ">" + theme.Date.Day + " " + month[theme.Date.Month - 1] + "</th>");
            }
            sw.WriteLine(@"</tr></thead><tbody>");
            foreach (var student in Students.OrderBy(e => e.FullName))
            {
                sw.WriteLine(@"<tr>");
                sw.WriteLine(@"<td class='name'><span>" + student.FullName + @"</span></td>");
                foreach (var theme in themes)
                {
                    var i = theme.Details.First(e => e.Student.Id == student.Id);
                    sw.WriteLine(@"<td" + (theme.IsExam == true ? " id='tdExam'" : "") + ">" + (i.IsAttend ? "г" : (i.Estimation == 0 || i.Estimation == null ? "" : i.Estimation.ToString())) + @"</td>");
                }
                sw.WriteLine(@"</tr>");
            }
            sw.WriteLine(@"</tbody></table><div></body></html>");
            sw.Close();

            if (RefreshWebPage != null) RefreshWebPage();
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    public class Quarter
    {
        public Quarter()
        {
        }

        public void Load(string filePath)
        {
            var fullText = new StreamReader(filePath, Encoding.UTF8).ReadToEnd().Split('\n');

            var arr = fullText[0].Split('|')[0].Split('.');
            Quarter1 = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));

            arr = fullText[1].Split('|')[0].Split('.');
            Quarter2 = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));

            arr = fullText[2].Split('|')[0].Split('.');
            Quarter3 = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));

            arr = fullText[3].Split('|')[0].Split('.');
            Quarter4 = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));

            arr = fullText[4].Split('|')[0].Split('.');
            End = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));
        }

        public DateTime Quarter1 { get; set; }
        public DateTime Quarter2 { get; set; }
        public DateTime Quarter3 { get; set; }
        public DateTime Quarter4 { get; set; }
        public DateTime End { get; set; }
    }

    public class StadyResult
    {
        public StadyResult(Subject sub, Student st, Quarter q)
        {
            Load(sub, st, q);
        }

        public string Student { get; set; }
        public int Quarter1 { get; set; }
        public int Quarter2 { get; set; }
        public int Quarter3 { get; set; }
        public int Quarter4 { get; set; }
        public int Year1 { get; set; }
        public int Year2 { get; set; }
        public int YearEnd { get; set; }

        public void Load(Subject sub, Student st, Quarter q)
        {
            Student = st.FullName;

            var all = st.Estimations.Where(e => e.Theme.Subject.Id == sub.Id && e.Theme.Date < q.Quarter2).ToList();
            var general = all.Where(e => e.IsAttend == false & e.Theme.IsExam == false).Sum(e => e.Estimation);
            var exam = all.Where(e => e.IsAttend == false & e.Theme.IsExam == true).Sum(e => e.Estimation);
            if (general == 0 && exam == 0)
                Quarter1 = 0;
            else if (general == 1 && exam == 0)
                Quarter1 = ((general / all.Count(e => e.Theme.IsExam == false)) / (all.Count(e => e.Theme.IsExam == true) * 2));
            else if (general == 0 && exam == 1)
                Quarter1 = (exam / all.Count(e => e.Theme.IsExam == true)) * 2 / (all.Count(e => e.Theme.IsExam == false));
            else
                Quarter1 = (exam / all.Count(e => e.Theme.IsExam == true)) * 2 + (general / all.Count(e => e.Theme.IsExam == false));

            if (Quarter1 > 5) Quarter1 = 5;


            all = st.Estimations.Where(e => e.Theme.Subject.Id == sub.Id & e.Theme.Date >= q.Quarter2 & e.Theme.Date < q.Quarter3).ToList();
            general = all.Where(e => e.IsAttend == false & e.Theme.IsExam == false).Sum(e => e.Estimation);
            exam = all.Where(e => e.IsAttend == false & e.Theme.IsExam == true).Sum(e => e.Estimation);
            if (general == 0 && exam == 0)
                Quarter2 = 0;
            else if (general == 1 && exam == 0)
                Quarter2 = ((general / all.Count(e => e.Theme.IsExam == false)) / (all.Count(e => e.Theme.IsExam == true) * 2));
            else if (general == 0 && exam == 1)
                Quarter2 = (exam / all.Count(e => e.Theme.IsExam == true)) * 2 / (all.Count(e => e.Theme.IsExam == false));
            else
                Quarter2 = (exam / all.Count(e => e.Theme.IsExam == true)) * 2 + (general / all.Count(e => e.Theme.IsExam == false));
            if (Quarter2 > 5) Quarter2 = 5;

            all = st.Estimations.Where(e => e.Theme.Subject.Id == sub.Id & e.Theme.Date >= q.Quarter3 & e.Theme.Date < q.Quarter4).ToList();
            general = all.Where(e => e.IsAttend == false & e.Theme.IsExam == false).Sum(e => e.Estimation);
            exam = all.Where(e => e.IsAttend == false & e.Theme.IsExam == true).Sum(e => e.Estimation);
            if (general == 0 && exam == 0)
                Quarter3 = 0;
            else if (general == 1 && exam == 0)
                Quarter3 = ((general / all.Count(e => e.Theme.IsExam == false)) / (all.Count(e => e.Theme.IsExam == true) * 2));
            else if (general == 0 && exam == 1)
                Quarter3 = (exam / all.Count(e => e.Theme.IsExam == true)) * 2 / (all.Count(e => e.Theme.IsExam == false));
            else
                Quarter3 = (exam / all.Count(e => e.Theme.IsExam == true)) * 2 + (general / all.Count(e => e.Theme.IsExam == false));
            if (Quarter3 > 5) Quarter3 = 5;


            all = st.Estimations.Where(e => e.Theme.Subject.Id == sub.Id && e.Theme.Date >= q.Quarter4).ToList();
            general = all.Where(e => e.IsAttend == false & e.Theme.IsExam == false).Sum(e => e.Estimation);
            exam = all.Where(e => e.IsAttend == false & e.Theme.IsExam == true).Sum(e => e.Estimation);
            var eC = all.Count(e => e.Theme.IsExam == true);
            var gC = all.Count(e => e.Theme.IsExam == false & (e.Estimation != 0 || e.IsAttend == true));

            if (general == 0 && exam == 0)
                Quarter4 = 0;
            else if (general > 0 && exam == 0)
            {
                Quarter4 = (general) / (gC + eC * 2);
            }
            else if (general == 0 && exam > 0)
            {
                Quarter4 = (exam * 2) / (gC + eC * 2);
            }
            else
                Quarter4 = (exam * 2 + general) / (gC + eC * 2);

            if (Quarter4 > 5) Quarter4 = 5;

            Year1 = (Quarter1 + Quarter2) / 2;
            Year2 = (Quarter3 + Quarter4) / 2;

            YearEnd = (Year1 + Year2) / 2;
        }
    }

    public class StudentResultStaty
    {
        public StudentResultStaty(Student st, List<Subject> subL)
        {
            Student = st;

            foreach (var sub in subL)
            {
                var newI = new StudentResultStatyItem() { Subject = sub.Name };
                var deteils = st.Estimations.Where(e => e.Estimation != 0 & e.Theme.Subject.Id == sub.Id);
                if (deteils.Count() > 0)
                {
                    newI.Avg = Math.Round((double)(deteils.Sum(e => e.Estimation) / deteils.Count()), 2);
                    newI.AvgProtsent = int.Parse((newI.Avg * 20).ToString());
                }
                else
                {
                    newI.Avg = 0;
                    newI.AvgProtsent = 0;
                }

                var themeCount = st.Estimations.Count(e => e.Theme.Subject.Id == sub.Id);
                var attendCount = st.Estimations.Count(e => e.Theme.Subject.Id == sub.Id & e.IsAttend == true);

                if (themeCount > 0 & attendCount > 0)
                {
                    newI.CountAttend = attendCount;
                    newI.ProtsentAttend = 100 - ((attendCount * 100) / themeCount);
                }
                else
                {
                    newI.CountAttend = 0;
                    newI.ProtsentAttend = 100;
                }
                Item.Add(newI);
            }
        }

        public Student Student { get; set; }

        public List<StudentResultStatyItem> Item { get; set; } = new List<StudentResultStatyItem>();
    }
    public class StudentResultStatyItem
    {
        public string Subject { get; set; }

        public double Avg { get; set; }

        public int AvgProtsent { get; set; }

        public int CountAttend { get; set; }

        public int ProtsentAttend { get; set; }
    }

}
