using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MontageScanLib;
using R24MontageScannerSqlAccess;



namespace MontageEingangScanUI;

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
        if (AuftragsListe.Items is INotifyCollectionChanged collection)
        {
            collection.CollectionChanged += (s, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    AuftragsListe.ScrollIntoView(args.NewItems[0]);
                }
            };
        }
    }



    private void eingangsScanTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (eingangsScanTextBox.Text.InputCheckLieferschein())
            {
                MontageLieferscheinModel eingabe = new MontageLieferscheinModel(eingangsScanTextBox.Text);
                angezeigteLieferscheine.Add(eingabe);

                
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
            if (KontrolleTextBox.Text.InputCheckLieferschein())
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
