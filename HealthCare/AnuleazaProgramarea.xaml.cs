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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare
{
    /// <summary>
    /// Interaction logic for AnuleazaProgramarea.xaml
    /// </summary>
    public partial class AnuleazaProgramarea : Window
    {
        string username;
        HealthCareEntities baza;
        int userID;
        public AnuleazaProgramarea(string username)
        {
            this.Left = 400;
            this.Top = 150;
            this.username = username;
            baza= new HealthCareEntities();
            InitializeComponent();

            var query = from utilizator in baza.Utilizatoris
                        where utilizator.UserName == this.username
                        select utilizator.UserID;

            int userID = query.FirstOrDefault();
            this.userID = userID;
            var rezultat_programari = from programari in baza.Programaris
                                      join medici in baza.Medicis on programari.MedicID equals medici.UserID
                                      where programari.PacientID == userID
                                      select medici.Nume + " " + medici.Prenume + " " + medici.Specializare + " " + programari.DataProgramare + " " + programari.OraProgramare;
                                     

            Programari.ItemsSource = rezultat_programari.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsWindow window = new AppointmentsWindow(this.username);
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Programari.SelectedItem != null)
            {
                var selectedProgramare = Programari.SelectedItem.ToString();
                string[] campuri = selectedProgramare.Split(' ');

                // Stocăm valorile din array în variabile
                string numeMedic = campuri[0];
                string prenumeMedic = campuri[1];
                string dataProgramare = campuri[3];
                string oraProgramare = campuri[4];

                var medic = from medici in baza.Medicis
                            where medici.Nume == numeMedic && medici.Prenume == prenumeMedic
                            select medici.UserID;

                int medicID = (int)medic.FirstOrDefault();

                if (medicID != 0)
                {
                    var newDisponibilitate = new Disponibilitati();
                    {
                        string valoare = dataProgramare + " " + oraProgramare;
                        newDisponibilitate.DataTimp = valoare;
                        newDisponibilitate.Doctor = medicID;
                    }

                    baza.Disponibilitatis.Add(newDisponibilitate);
                    baza.SaveChanges();

                    var programare = from programari in baza.Programaris
                                     where programari.DataProgramare == dataProgramare && programari.OraProgramare == oraProgramare && programari.MedicID == medicID && programari.PacientID == this.userID
                                     select programari.ProgramareID;

                    int programareID = programare.FirstOrDefault();

                    var elementDeSters = baza.Programaris.FirstOrDefault(element => element.ProgramareID == programareID);

                    if (elementDeSters != null)
                    {
                        baza.Programaris.Remove(elementDeSters);
                        baza.SaveChanges();
                    }

                    MessageBox.Show("Programare anulată cu succes!");
                }
            }
        }
    }
}
