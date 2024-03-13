using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MontageScanLib;
using R24MontageScannerSqlAccess;
using R24MontageScannerSqlAccess.Models;
using R24MontageScannerUI;



namespace MontageEingangScanUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    BindingList<EingangsLieferscheinModel> angezeigteLieferscheine = new BindingList<EingangsLieferscheinModel>();
    SqlLieferschein sqlLieferschein;


    public MainWindow()
    {
        InitializeComponent();
        sqlLieferschein = new SqlLieferschein(getConnectionString());
       
       
        //CsvManager.FillListWithLastEntrys(angezeigteLieferscheine, 100);
        AuftragsListe.ItemsSource = angezeigteLieferscheine;

    }
    private string getConnectionString()
    {
        //Get Connectionstring from config
        return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrainingMontageScan;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrainingMontageScan;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False

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
            if (eingangsScanTextBox.Text.inputCheckLieferschein())
            {
                EingangsLieferscheinModel lieferscheinScan = new EingangsLieferscheinModel(eingangsScanTextBox.Text);
                if (lieferscheinExistsCheck(lieferscheinScan) == true)
                {
                    MessageBox.Show("Lieferschein bereits gescannt. Datum aktualisiert");
                    sqlLieferschein.UpdateLieferschein(lieferscheinScan);
                    angezeigteLieferscheine.Add(lieferscheinScan);

                    uiCleanUp();
                }
                else
                {
                    sqlLieferschein.LieferscheinEingangsScan(lieferscheinScan);
                    angezeigteLieferscheine.Add(lieferscheinScan);
                    uiCleanUp();
                }

            }
            else
            {

                WrongInputAlarm(eingangsScanTextBox);
            }

        }
    }

    private void uiCleanUp()
    {
        eingangsScanTextBox.Background = Brushes.White;
        eingangsScanTextBox.Clear();
        AuftragsListe.ScrollIntoView(AuftragsListe.Items[AuftragsListe.Items.Count - 1]);
    }
    private bool lieferscheinExistsCheck(EingangsLieferscheinModel input)
    {
        bool output;
        try
        {
            sqlLieferschein.SucheNachLieferschein(input.Lieferschein);
            output = true;
        }
        catch 
        {
            output = false;
        }
        return output;
    }
    private void KontrolleTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (KontrolleTextBox.Text.inputCheckLieferschein())
            { 
                string searchResult = "Lieferschein nicht gefunden";

                try 
                {
                    searchLieferschein found = sqlLieferschein.SucheNachLieferschein(KontrolleTextBox.Text);
                    if (found.Lieferschein == KontrolleTextBox.Text)
                    {
                        searchResult = $"{found.Lieferschein} \nKommissionierung: {found.EingangsTS} \nMontage: {found.MontageTS}";
                    }
                } 
                catch 
                {
                   
                }
               
                MessageBox.Show(searchResult);
                
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

    private void neuesMitarbeiterFenster(object sender, RoutedEventArgs e)
       
    {
        

        AddUpdateUser addUser = new AddUpdateUser(getConnectionString());
        addUser.Show();
    }

    private void MontageFenser(object sender, RoutedEventArgs e)
    {
        MonteurScanner montageScanner = new MonteurScanner(getConnectionString());
        montageScanner.Show();
    }
}
