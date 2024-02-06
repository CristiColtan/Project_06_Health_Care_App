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

namespace HealthCare
{
    public partial class AdaugaMedicament : Window
    {
        string username;
        int paciendID;
        HealthCareEntities baza;
        public AdaugaMedicament(string username,int pacientID)
        {
            this.Left = 400;
            this.Top = 150;
            this.username = username;   
            this.paciendID = pacientID;
            baza=new HealthCareEntities();
            InitializeComponent();


            var medicament = from medicamente in baza.Medicamentes
                             select medicamente.Denumire + " " + medicamente.Cantitate + " " + medicamente.Gramaj;

            Medicament.ItemsSource = medicament.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientiiMei window = new PacientiiMei(username);
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (Medicament.SelectedItem != null && Medicament.SelectedItem.ToString() != null)
            {
                string text = Medicament.SelectedItem.ToString();
                string[] campuri = text.Split(' ');

                // Salvare valori în variabile locale
                string denumireMedicament = campuri[0];
                string cantitateMedicament = campuri[1];
                string gramajMedicament = campuri[2];

                var idMedicament = from medicamente in baza.Medicamentes
                                   where medicamente.Denumire == denumireMedicament &&
                                         medicamente.Cantitate.ToString() == cantitateMedicament &&
                                         medicamente.Gramaj == gramajMedicament
                                   select medicamente.MedicamentID;

                // Utilizare valori salvate în variabile locale
                int id = idMedicament.FirstOrDefault();

                var newMedicament = new Medicamente_pacienti();
                {
                    newMedicament.Pacienti_ID = this.paciendID;
                    newMedicament.Medicament_ID = id;
                    if (Data_incepere.SelectedDate.HasValue)
                        newMedicament.DataIncepere = Data_incepere.SelectedDate.Value;
                    else
                    {
                        MessageBox.Show("Nu ati selectat data de incepere tratament!");
                        AdaugaMedicament window = new AdaugaMedicament(this.username, this.paciendID);
                        window.Show();
                        this.Close();
                    }
                    if (Data_incheiere.SelectedDate.HasValue)
                        newMedicament.DataIncheiere = Data_incheiere.SelectedDate.Value;
                    else
                    {
                        MessageBox.Show("Nu ati selectat data de incheiere tratament!");
                        AdaugaMedicament window = new AdaugaMedicament(this.username, this.paciendID);
                        window.Show();
                        this.Close();
                    }
                }

                baza.Medicamente_pacienti.Add(newMedicament);
                baza.SaveChanges();
                MessageBox.Show("Medicament adaugat cu succes!");
            }

        }


    }
}
