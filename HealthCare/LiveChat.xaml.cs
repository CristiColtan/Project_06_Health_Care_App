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
    /// <summary>
    /// Interaction logic for LiveChat.xaml
    /// </summary>
    public partial class LiveChat : Window
    {

        string username;
        public LiveChat(string username)
        {
            this.Left = 400;
            this.Top = 150;
            InitializeComponent();
            this.username = username;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Va sfatuim sa va faceti o programare la CARDIOLOGIE!");
            MakeAppointment window = new MakeAppointment(this.username);
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Va sfatuim sa va faceti o programare la MEDICINA DE FAMILIE!");
            MakeAppointment window = new MakeAppointment(this.username);
            window.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Va sfatuim sa va faceti o programare la NEUROLOGIE!");
            MakeAppointment window = new MakeAppointment(this.username);
            window.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PacientWindow window=new PacientWindow(this.username);
            window.Show();
            this.Close();
        }
    }
}
