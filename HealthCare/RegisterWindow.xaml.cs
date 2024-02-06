using System;
using System.Collections.Generic;
using System.Globalization;
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
using static System.Net.Mime.MediaTypeNames;

namespace HealthCare
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        HealthCareEntities baza;
        public RegisterWindow()
        {
            this.Left = 400;
            this.Top = 150;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           baza=new HealthCareEntities();
            string username_save;
          
                var newUtilizator = new Utilizatori();
                {
                    CultureInfo culture = CultureInfo.CurrentCulture;
                    newUtilizator.Nume = Nume_register.txtInput.Text;
                    newUtilizator.Prenume = Prenume_register.txtInput.Text;
                    newUtilizator.DataNasterii =Data_nastere_register.txtInput.Text ;
                    newUtilizator.Email = Email_register.txtInput.Text;
                    newUtilizator.Parola = Parola_register.txtInput.Text;
                    newUtilizator.CNP = CNP_register.txtInput.Text;
                    string cnpText = CNP_register.txtInput.Text;
                    newUtilizator.UserName=Nume_register.txtInput.Text+"."+Prenume_register.txtInput.Text+ cnpText.Substring(10);
                    username_save=newUtilizator.UserName;
                    newUtilizator.Doctor = false;

                }
                baza.Utilizatoris.Add(newUtilizator);
                baza.SaveChanges();
            
            string mesaj = "Username generat:" + username_save + "\n" + "Înregistrare cu succes!";
            MessageBox.Show(mesaj);
            LogInWindowxaml windowxaml = new LogInWindowxaml();
            this.Close();
            windowxaml.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }
    }
}
