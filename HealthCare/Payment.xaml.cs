using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        string username;
        public Payment(string username)
        {
            this.Left = 400;
            this.Top = 150;
            InitializeComponent();
            this.username = username;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientWindow window = new PacientWindow(this.username);
            window.Show();  
            this.Close();   
        }
    }
}
