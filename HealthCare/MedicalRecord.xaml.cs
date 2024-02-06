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
    /// <summary>
    /// Interaction logic for MedicalRecord.xaml
    /// </summary>
    public partial class MedicalRecord : Window
    {
        string Username;
        HealthCareEntities baza;
        public MedicalRecord(string username)
        {
            this.Left = 400;
            this.Top = 150;
            this.Username = username;
            baza=new HealthCareEntities();
            InitializeComponent();
       

                var query = from utilizator in baza.Utilizatoris
                            where utilizator.UserName == this.Username
                            select utilizator.UserID;

                int userID = query.FirstOrDefault();

            
                var rezultat_diag = from diagnostice in baza.Diagnostices
                               join diagPacienti in baza.Diagnostice_pacienti on diagnostice.DiagnosticID equals diagPacienti.Diagnostic_ID
                               where diagPacienti.Pacienti_ID== userID
               
                               select new
                               {
                                   Denumire = diagnostice.Denumire,
                                   Tip = diagnostice.Tip,
                                   Data = diagPacienti.DataDiagnostic
                               };
                
                Diagnostice.ItemsSource=rezultat_diag.ToList();
                var rezultat_tratament=from medicamente in baza.Medicamentes
                                       join medPacienti in baza.Medicamente_pacienti on medicamente.MedicamentID equals medPacienti.Medicament_ID
                                      where  medPacienti.Pacienti_ID==userID
                                       select new
                                       {
                                           Denumire =medicamente.Denumire,
                                           Gramaj=medicamente.Gramaj,
                                           Cantitate=medicamente.Cantitate,
                                           DataIncepere = medPacienti.DataIncepere,
                                           DataIncheiere=medPacienti.DataIncheiere
                                       };
                Tratament.ItemsSource = rezultat_tratament.ToList();

                var rezultat_analize = from analize in baza.Analizes
                                         join analizePacienti in baza.Analize_pacienti on analize.AnalizaID equals analizePacienti.Analiza_ID
                                        where analizePacienti.Pacient_ID==userID
                                         select new
                                         {
                                             Denumire = analize.Denumire,
                                             Parametrii = analize.Parametrii,
                                             Rezultat=analizePacienti.Rezultat
                                             
                                         };
                Analize.ItemsSource = rezultat_analize.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientWindow window = new PacientWindow(this.Username);
            window.Show();
            this.Close();
        }

    }
}
