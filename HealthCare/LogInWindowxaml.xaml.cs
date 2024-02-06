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
    public partial class LogInWindowxaml : Window
    {
        HealthCareEntities baza;
        public LogInWindowxaml()
        {
            this.Left = 400;
            this.Top = 150;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            baza=new HealthCareEntities();
            

                IEnumerable<dynamic> utilizatori = from utilizator in baza.Utilizatoris
                                                   select utilizator;
                bool found = false;

                foreach (dynamic utilizator in utilizatori)
                {
                    if (utilizator.UserName == Username_login.txtInput.Text && utilizator.Parola == Parola_login.txtInput.Text)
                    {
                        MessageBox.Show("Conectare cu succes!");
                        bool valoare = utilizator.Doctor;
                        if (valoare)
                        {
                           
                            DoctorWindow window = new DoctorWindow(utilizator.UserName); 
                            this.Close();
                            window.Show();
                        }
                        else
                        {
                            
                            PacientWindow windoe = new PacientWindow(utilizator.UserName);
                            this.Close();
                            windoe.Show();
                        }
                        found = true;
                        break;
                    }
                }
                   if(found==false)
                    {
                        MessageBox.Show("Nume de utilizator sau parola gresita!");
                    }
                
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window= new MainWindow();
            this.Close();
            window.Show();
        }
    }
}
