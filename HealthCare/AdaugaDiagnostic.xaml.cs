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
    public partial class AdaugaDiagnostic : Window
    {
        string username;
        int idPacient;
        HealthCareEntities baza;
        public AdaugaDiagnostic(string username, int idPacient)
        {
            this.Left = 400;
            this.Top = 150;
            this.username = username;
            this.idPacient = idPacient;
            baza = new HealthCareEntities();
            InitializeComponent();


            var diagnostic = from diagnostice in baza.Diagnostices
                             select diagnostice.Denumire + " " + diagnostice.Tip;

            Diagnostice.ItemsSource = diagnostic.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientiiMei window = new PacientiiMei(this.username);
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Diagnostice.SelectedItem != null && Diagnostice.SelectedItem.ToString() != null)
            {
                string text = Diagnostice.SelectedItem.ToString();
                string[] campuri = text.Split(' ');

       
                string denumireDiagnostic = campuri[0];
                string tipDiagnostic = campuri[1];

                var idDiagnostic = from diagnostice in baza.Diagnostices
                                   where diagnostice.Denumire == denumireDiagnostic && diagnostice.Tip == tipDiagnostic
                                   select diagnostice.DiagnosticID;

               
                int id = idDiagnostic.FirstOrDefault();

                var newDiagnostic = new Diagnostice_pacienti();
                {
                    newDiagnostic.Pacienti_ID = this.idPacient;
                    newDiagnostic.Diagnostic_ID = id;
                    if (Data.SelectedDate.HasValue)
                        newDiagnostic.DataDiagnostic = Data.SelectedDate.Value;
                    else
                    {
                        MessageBox.Show("Nu ati selectat data diagnostic!");
                        AdaugaMedicament window = new AdaugaMedicament(this.username, this.idPacient);
                        window.Show();
                        this.Close();
                    }
                }

                baza.Diagnostice_pacienti.Add(newDiagnostic);
                baza.SaveChanges();
                MessageBox.Show("Diagnostic adaugat cu succes!");
            }

        }
    }
}
