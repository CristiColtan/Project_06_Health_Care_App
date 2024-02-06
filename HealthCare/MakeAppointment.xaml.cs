using System;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HealthCare
{
    /// <summary>
    /// Interaction logic for MakeAppointment.xaml
    /// </summary>
    public partial class MakeAppointment : Window
    {
        string username;
        int idUser;
        int idMedic;
        int idDisp;
        HealthCareEntities baza;
        public MakeAppointment(string Username)
        {
            this.Left = 400;
            this.Top = 150;
            InitializeComponent();
            this.username = Username;
            baza = new HealthCareEntities();
            InitializeComponent();
         

                var query = from utilizator in baza.Utilizatoris
                            where utilizator.UserName == this.username
                            select utilizator.UserID;

                int userID = query.FirstOrDefault();
                this.idUser = userID;

                var specializari = from medici in baza.Medicis
                                   select medici.Specializare;
            
                Specializari.ItemsSource = specializari.ToList();

            
        }

        private void Specializari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
                var medici = from medic in baza.Medicis
                             where medic.Specializare == Specializari.SelectedItem.ToString()
                             select medic.Nume + " " + medic.Prenume;
                Medici_lista.ItemsSource = medici.ToList();
            
        }


        private void Medici_lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Medici_lista.SelectedItem != null && Medici_lista.SelectedItem.ToString() != null)
            {
                string text = Medici_lista.SelectedItem.ToString();
                string[] numePrenume = text.Split(' ');

                string numeMedic = numePrenume[0];
                string prenumeMedic = numePrenume[1];

                var utilizatori = from utilizator in baza.Utilizatoris
                                  where utilizator.Nume == numeMedic && utilizator.Prenume == prenumeMedic
                                  select utilizator.UserID;

                int medicID = utilizatori.SingleOrDefault();
                this.idMedic = medicID;

                var disponibilitate = from disponibilitati in baza.Disponibilitatis
                                      where disponibilitati.Doctor == medicID
                                      select disponibilitati.DataTimp;
                Data_si_ora.ItemsSource = disponibilitate.ToList();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (Data_si_ora.SelectedItem != null)
            {
                string text1 = Data_si_ora.SelectedItem.ToString();

               
                    var disponibilitate1 = from disponibilitati in baza.Disponibilitatis
                                           where disponibilitati.Doctor == idMedic && disponibilitati.DataTimp == text1
                                           select disponibilitati.DisponibilitateID;
                   this.idDisp = disponibilitate1.FirstOrDefault();
                
            }
            var newProgramare = new Programari();
            {

                string text = Data_si_ora.SelectedItem.ToString();
                string[] dataOra = text.Split(' ');
                newProgramare.PacientID = this.idUser;
                newProgramare.MedicID = this.idMedic;
                newProgramare.DataProgramare = dataOra[0];
                newProgramare.OraProgramare= dataOra[1];

            }
            baza.Programaris.Add(newProgramare);
            baza.SaveChanges();

            string mesaj = "Programare realizata cu succes!";
            MessageBox.Show(mesaj);


            var elementDeSters = baza.Disponibilitatis.FirstOrDefault(element => element.DisponibilitateID == idDisp);

            if (elementDeSters != null)
            {
                baza.Disponibilitatis.Remove(elementDeSters);
                baza.SaveChanges(); 
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PacientWindow window = new PacientWindow(this.username);
            window.Show();
            this.Close();
        }
    }
}
