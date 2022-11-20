using ClassJoural.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;

namespace ClassJoural
{
    public partial class MainWindow : Window
    {
        AppViewModel appViewModel;
        static SerialPort port = new SerialPort();
        string curDir = Directory.GetCurrentDirectory();
        public MainWindow()
        {
            InitializeComponent();
            WebList.Navigate(new Uri(curDir + @"\index.html", UriKind.Absolute));
            appViewModel = new AppViewModel(WebList);
            appViewModel.RefreshWebPage = RefreshWebPage;
            RefreshWebPage();
            DataContext = appViewModel;
        }
        void RefreshWebPage()
        {
            WebList.Navigate(new Uri(curDir + @"\index.html"));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //appViewModel.DataProvider.DeleteDb();
                // appViewModel = new AppViewModel(WebList);
                // DataContext = appViewModel;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void MenuBtn_Checked(object sender, RoutedEventArgs e)
        {
            MenuBtn.IsChecked = false;
            MenuPopup.IsOpen = true;
        }
        private void AddParentForStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Parents.SelectedIndex != -1 && StudentDataList.SelectedIndex != -1 && !(StudentDataList.SelectedItem as Student).Parents.Contains(Parents.SelectedItem as Parent))
            {
                (StudentDataList.SelectedItem as Student).Parents.Add(Parents.SelectedItem as Parent);
            }
        }
        private void DeleteParentFromStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StudentParents.SelectedIndex != -1)
            {
                (StudentDataList.SelectedItem as Student).Parents.Remove(StudentParents.SelectedItem as Parent);
            }
        }
        private void AddNewParentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewParantPhone.Text != "" & NewParantFIO.Text != "")
            {
                var newParent = appViewModel.DataProvider.Add<Parent>();
                newParent.FIO = NewParantFIO.Text; newParent.Phone = NewParantPhone.Text;
                appViewModel.Parents.Add(newParent);
                NewParantPhone.Text = "";
                NewParantFIO.Text = "";
            }
            else
                MessageBox.Show("Маълумотҳоро ба пурраги дохил кунед!");
        }
        private void PopupExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        MessageBoxResult ExitBox()
        {
            return MessageBox.Show("Мехоҳед тағйиротҳои охир сабт карда шавад?", "Огоҳи", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        }
        private void PopupSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            var d = appViewModel.DataProvider.EFContext.ChangeTracker;
            var de = appViewModel.DataProvider.EFContext.ChangeTracker.Entries();
            var dec = appViewModel.DataProvider.EFContext.ChangeTracker.Entries().Where(en => en.State == EntityState.Added || en.State == EntityState.Deleted || en.State == EntityState.Modified);
        }
        private void thisPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var res = ExitBox();
            if (res == MessageBoxResult.Cancel)
                e.Cancel = true;
            else
                appViewModel.DataProvider.SaveChanges();
        }
        private void SendStudentResultBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SendSMSBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StudentListForSentSMS.SelectedItems.Count != 0)
            {
                OpenPort();
                bool result;
                result = sendSMS("Hello phone!", "981042597");
                port.Close();
            }
        }

        private bool sendSMS(string textsms, string telnumber)
        {
            if (!port.IsOpen) return false;

            try
            {
                System.Threading.Thread.Sleep(500);
                port.WriteLine("AT\r\n"); // означает "Внимание!" для модема 
                System.Threading.Thread.Sleep(500);

                port.Write("AT+CMGF=1\r\n"); // устанавливается текстовый режим для отправки сообщений
                System.Threading.Thread.Sleep(500);
            }
            catch { return false; }

            try
            {
                port.Write("AT+CMGS=\"" + telnumber + "\"" + "\r\n"); // задаем номер мобильного телефона получаталя смс
                System.Threading.Thread.Sleep(500);

                port.Write(textsms + char.ConvertFromUtf32(26) + "\r\n"); // отправляем текст смс
                System.Threading.Thread.Sleep(3500);
            }
            catch { return false; }

            try
            {
                string recievedData;
                recievedData = port.ReadExisting();

                if (recievedData.Contains("ERROR"))
                {
                    return false;
                }

            }
            catch { return false; }

            return true;
        }

        private void OpenPort()
        {

            port.BaudRate = 2400;
            port.DataBits = 7;

            port.StopBits = StopBits.One;
            port.Parity = Parity.Odd;

            port.ReadTimeout = 500;
            port.WriteTimeout = 500;

            port.Encoding = Encoding.GetEncoding("windows-1251");

            port.PortName = "COM16";


            if (port.IsOpen)
                port.Close();
            try
            {
                port.Open();
            }
            catch (Exception ex) {}

        }

    }
}
