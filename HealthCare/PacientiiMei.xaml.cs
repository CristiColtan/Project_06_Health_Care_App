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
    public partial class PacientiiMei : Window
    {
        string username;
        int idUser;
        int idPacient;
        HealthCareEntities baza;
        public PacientiiMei(string Username)
        {
            this.Left = 400;
            this.Top = 150;
            this.username = Username;
            baza= new HealthCareEntities();
            InitializeComponent();


            var medic = from utilizatori in baza.Utilizatoris
                           where utilizatori.UserName == this.username
                           select utilizatori.UserID;


            idUser = medic.FirstOrDefault();

            var pacienti = (from programari in baza.Programaris
                            join utilizatori in baza.Utilizatoris on programari.PacientID equals utilizatori.UserID
                            where programari.MedicID == this.idUser
                            select utilizatori.UserID).Distinct();


            var numePrenumeDistincte = (from utilizator in baza.Utilizatoris
                                        where pacienti.Contains(utilizator.UserID)
                                        select utilizator.Nume + " " + utilizator.Prenume).ToList();

            
            foreach (var numePrenume in numePrenumeDistincte)
            {
                Pacienti.Items.Add(numePrenume);
            }
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorWindow window= new DoctorWindow(this.username);
            window.Show();
            this.Close();
        }

        private void Pacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = Pacienti.SelectedItem.ToString();
            string[] numePrenume = text.Split(' ');
            string numePacient = numePrenume[0];
            string prenumePacient = numePrenume[1];

            var query = from utilizator in baza.Utilizatoris
                        where utilizator.Nume == numePacient && utilizator.Prenume == prenumePacient
                        select utilizator.UserID;

            int userID = query.FirstOrDefault();
            this.idPacient = userID;

            var rezultat_diag = from diagnostice in baza.Diagnostices
                                join diagPacienti in baza.Diagnostice_pacienti on diagnostice.DiagnosticID equals diagPacienti.Diagnostic_ID
                                where diagPacienti.Pacienti_ID==userID

                                select new
                                {
                                    Denumire = diagnostice.Denumire,
                                    Tip = diagnostice.Tip,
                                    Data = diagPacienti.DataDiagnostic
                                };

            Diagnostice.ItemsSource = rezultat_diag.ToList();
            var rezultat_tratament = from medicamente in baza.Medicamentes
                                     join medPacienti in baza.Medicamente_pacienti on medicamente.MedicamentID equals medPacienti.Medicament_ID
                                     where medPacienti.Pacienti_ID==userID
                                     select new
                                     {
                                         Denumire = medicamente.Denumire,
                                         Gramaj = medicamente.Gramaj,
                                         Cantitate = medicamente.Cantitate,
                                         DataIncepere = medPacienti.DataIncepere,
                                         DataIncheiere = medPacienti.DataIncheiere
                                     };
            Tratament.ItemsSource = rezultat_tratament.ToList();

            var rezultat_analize = from analize in baza.Analizes
                                   join analizePacienti in baza.Analize_pacienti on analize.AnalizaID equals analizePacienti.Analiza_ID
                                   where analizePacienti.Pacient_ID==userID
                                   select new
                                   {
                                       Denumire = analize.Denumire,
                                       Parametrii = analize.Parametrii,
                                       Rezultat = analizePacienti.Rezultat

                                   };
            Analize.ItemsSource = rezultat_analize.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdaugaAnaliza window=new AdaugaAnaliza(this.username, this.idPacient);
            window.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AdaugaMedicament window=new AdaugaMedicament(this.username, this.idPacient);    
            window.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AdaugaDiagnostic window = new AdaugaDiagnostic(this.username, this.idPacient);
            window.Show();
            this.Close();
        }

    }
}
