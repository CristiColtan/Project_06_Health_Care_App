using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    public partial class DoctorWindow : Window
    {
        string Username;
        HealthCareEntities baza;
        public DoctorWindow(string username)
        {
            this.Left = 400;
            this.Top = 150;
            baza=new HealthCareEntities();
            InitializeComponent();
            Username = username;
            string text = "Logged as: " + username;
            Logged.Text = text;

            var query = from utilizator in baza.Utilizatoris
                        where utilizator.UserName == this.Username
                        select utilizator.UserID;

            int userID = query.FirstOrDefault();

            var rezultat_programari = from programari in baza.Programaris
                                      join medici in baza.Medicis on programari.MedicID equals medici.UserID
                                      join utilizatori in baza.Utilizatoris on programari.PacientID equals utilizatori.UserID   
                                      where programari.MedicID == userID

                                      select new
                                      {
                                          NumePacient = utilizatori.Nume,
                                          PrenumePacient = utilizatori.Prenume,
                                          Specializare = medici.Specializare,
                                          DataProgramare = programari.DataProgramare,
                                          OraProgramare = programari.OraProgramare
                                      };

            Programari_afis.ItemsSource = rezultat_programari.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientiiMei window = new PacientiiMei(this.Username);
            window.Show();  
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LogInWindowxaml window = new LogInWindowxaml();
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DisponibilitatiWindow disponibilitati = new DisponibilitatiWindow(this.Username);
            disponibilitati.Show();
            this.Close();
        }
    }
}
