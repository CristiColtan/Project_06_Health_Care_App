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
    public partial class AppointmentsWindow : Window
    {
        string username;
        HealthCareEntities baza;
        public AppointmentsWindow(string username)
        {
            this.Left = 400;
            this.Top = 150;
            InitializeComponent();
            this.username = username;
            baza = new HealthCareEntities();
            InitializeComponent();

            var query = from utilizator in baza.Utilizatoris
                        where utilizator.UserName == this.username
                        select utilizator.UserID;

            int userID = query.FirstOrDefault();

            var rezultat_programari = from programari in baza.Programaris
                                      join medici in baza.Medicis on programari.MedicID equals medici.UserID
                                      where programari.PacientID == userID
                                      select new
                                      {
                                          NumeMedic = medici.Nume,
                                          PrenumeMedic = medici.Prenume,
                                          Specializare = medici.Specializare,
                                          DataProgramare = programari.DataProgramare,
                                          OraProgramare = programari.OraProgramare
                                      };

            Programari_viitoare.ItemsSource = rezultat_programari.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientWindow window = new PacientWindow(this.username);
            this.Close();
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AnuleazaProgramarea window=new AnuleazaProgramarea(this.username);
            window.Show();
            this.Close();
        }
    }
}
