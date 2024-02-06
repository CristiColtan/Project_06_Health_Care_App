using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare
{
    public partial class DisponibilitatiWindow : Window
    {
        string username;
        HealthCareEntities baza;
        public DisponibilitatiWindow(string Username)
        {
            this.Left = 400;
            this.Top = 150;
            this.username=Username;
            baza=new HealthCareEntities();
            InitializeComponent();
            Ora.Items.Add("09:00");
            Ora.Items.Add("10:00");
            Ora.Items.Add("11:00");
            Ora.Items.Add("12:00");
            Ora.Items.Add("13:00");
            Ora.Items.Add("14:00");
            Ora.Items.Add("15:00");
            Ora.Items.Add("16:00");
            Ora.Items.Add("17:00");
            Ora.Items.Add("18:00");

            var disponibilitate = from disponibilitati in baza.Disponibilitatis
                                  join utilizatori in baza.Utilizatoris on disponibilitati.Doctor equals utilizatori.UserID
                                  where utilizatori.UserName == this.username
                                  select new
                                  {
                                      Disponibilitate = disponibilitati.DataTimp
                                  };
            Disponibilitati_.ItemsSource = disponibilitate.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorWindow window = new DoctorWindow(this.username);
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var utilizatorQuery = from utilizatori in baza.Utilizatoris
                                  where utilizatori.UserName == this.username
                                  select utilizatori.UserID;

            int ID = utilizatorQuery.FirstOrDefault();

            var newDisponibilitate = new Disponibilitati();
            {
                string text = Data.SelectedDate.Value.ToString();
                string[] campuri = text.Split(' ');

             
                string dataPart = campuri[0];
                string oraPart = Ora.Text;

      
                string valoare = dataPart + " " + oraPart;
                newDisponibilitate.DataTimp = valoare;
                newDisponibilitate.Doctor = ID;
            }

            baza.Disponibilitatis.Add(newDisponibilitate);
            baza.SaveChanges();
            MessageBox.Show("Disponibilitate adaugata cu succes!");

        }
    }
}
