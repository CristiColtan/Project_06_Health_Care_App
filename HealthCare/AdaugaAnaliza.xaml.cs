using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
   
    public partial class AdaugaAnaliza : Window
    {
        string username;
        int idPacient;
        HealthCareEntities baza;
        public AdaugaAnaliza(string username, int idPacient)
        {

            this.Left = 400;
            this.Top = 150;
            this.username = username;
            this.idPacient = idPacient;
            baza = new HealthCareEntities();
            InitializeComponent();


            var analiza = from analize in baza.Analizes
                             select analize.Denumire + " " + analize.Parametrii;

            Analize.ItemsSource = analiza.ToList();

            Rezultat.Items.Add("Normal");
            Rezultat.Items.Add("Mic");
            Rezultat.Items.Add("Mare");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientiiMei window = new PacientiiMei(this.username);
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Analize.SelectedItem != null && Analize.SelectedItem.ToString() != null)
            {
                string text = Analize.SelectedItem.ToString();
                string[] campuri = text.Split(' ');

                string denumireAnaliza = campuri[0];
                string parametriiAnaliza = campuri[1];

                var idAnaliza = from analize in baza.Analizes
                                where analize.Denumire == denumireAnaliza && analize.Parametrii == parametriiAnaliza
                                select analize.AnalizaID;

          
                int id = idAnaliza.FirstOrDefault();

                var newAnaliza = new Analize_pacienti();
                {
                    newAnaliza.Pacient_ID = this.idPacient;
                    newAnaliza.Analiza_ID = id;
                    newAnaliza.Rezultat = Rezultat.Text;
                }

                baza.Analize_pacienti.Add(newAnaliza);
                baza.SaveChanges();
                MessageBox.Show("Analiza adaugata cu succes!");
            }

        }
    }
}
