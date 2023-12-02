using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MontageScanLib;



namespace MontageEingangScanUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<MontageLieferscheinModel> angezeigteLieferscheine = new BindingList<MontageLieferscheinModel>();



        public MainWindow()
        {
            InitializeComponent();

            CsvManager.CreateCsvFile();
            CsvManager.FillListWithLastEntrys(angezeigteLieferscheine, 100);
            AuftragsListe.ItemsSource = angezeigteLieferscheine;

        }

        private void AuftragsListe_Loaded(object sender, RoutedEventArgs e)
        {
            AuftragsListe.ScrollIntoView(AuftragsListe.Items[AuftragsListe.Items.Count - 1]);
        }


        //CsvManager csvManager = new CsvManager();
        private void eingangsScanTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (eingangsScanTextBox.Text.InputCheck())
                {
                    MontageLieferscheinModel eingabe = new MontageLieferscheinModel(eingangsScanTextBox.Text);
                    angezeigteLieferscheine.Add(eingabe);

                    //csvManager.StoreLieferschein(eingabe);
                    CsvManager.WriteToCsv(eingabe);
                    eingangsScanTextBox.Background = Brushes.White;
                    eingangsScanTextBox.Clear();
                    AuftragsListe.ScrollIntoView(AuftragsListe.Items[AuftragsListe.Items.Count - 1]);
                }
                else
                {

                    WrongInputAlarm(eingangsScanTextBox);
                }

            }
        }
        private void KontrolleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (KontrolleTextBox.Text.InputCheck())
                {

                    MessageBox.Show(CsvManager.SearchForLS(KontrolleTextBox.Text));
                    KontrolleTextBox.Clear();
                    KontrolleTextBox.Background = Brushes.White;
                }
                else
                {
                    WrongInputAlarm(KontrolleTextBox);
                }
            }

        }


        private void WrongInputAlarm(TextBox sender)
        {
            sender.Background = Brushes.Red;
            sender.Clear();
            sender.Focus();
        }
    }


}
