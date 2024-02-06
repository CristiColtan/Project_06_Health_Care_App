using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HealthCare
{
    public partial class PacientWindow : Window
    {
        string Username;
        public PacientWindow(string username)
        {
            this.Left = 400;
            this.Top = 150;
            Username = username;
            string text = "Logged as: " + username;
            InitializeComponent();
            Logged.Text = text;
        }

        private void btnLiveChat_Click(object sender, RoutedEventArgs e)
        {
            LiveChat window=new LiveChat(this.Username);
            window.Show();
            this.Close();
        }

        private void btnAppointments_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsWindow window = new AppointmentsWindow(Username);
            this.Close();
            window.Show();
        }

        private void btnMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord wind = new MedicalRecord(Username);
            this.Close();
            wind.Show();

        }

        private void btnMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointment window=new MakeAppointment(Username);
            window.Show();
            this.Close();
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            Payment window=new Payment(Username);
            window.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window= new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
