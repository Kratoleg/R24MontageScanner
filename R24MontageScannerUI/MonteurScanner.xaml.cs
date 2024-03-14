using MontageScanLib;
using R24MontageScannerSqlAccess;
using System.Windows;
using System.Windows.Media;
using R24MontageScannerSqlAccess.Models;
using System.ComponentModel;
using MontageScannerLibrary;
using System.Windows.Input;



namespace R24MontageScannerUI;

public partial class MonteurScanner : Window
{
    private MitarbeiterModel _loggedInMitarbeiter;
    private SqlMitarbeiter _sqlMa;

    private SqlLieferschein _sqlLs;
    private MontageLieferscheinModel _lieferscheinInput;
    BindingList<DisplayedModel> angezeigteLieferscheine;
    public MonteurScanner(string connectionString)
    {
        //GetConnectionString
        _sqlMa = new SqlMitarbeiter(connectionString);
        _sqlLs = new SqlLieferschein(connectionString);
        angezeigteLieferscheine = new BindingList<DisplayedModel>();
        InitializeComponent();
        //Display a few Values inside the ListBox
        DisplayName.Text = "Kein Mitarbeiter";
        AuftragsListe.ItemsSource = angezeigteLieferscheine;

    }



    private void saveLieferscheinToDb(string lieferschein)
    {
        _lieferscheinInput = new MontageLieferscheinModel(lieferschein);

        //If Inputscan true update
        try
        {
            _sqlLs.SucheNachLieferschein(lieferschein);
            _sqlLs.LieferscheinMontageScan(_lieferscheinInput, _loggedInMitarbeiter.Id);
        }
        catch
        {
            EingangsLieferscheinModel input = new EingangsLieferscheinModel(lieferschein);

            _sqlLs.LieferscheinEingangsScan(input);
            _sqlLs.LieferscheinMontageScan(_lieferscheinInput, _loggedInMitarbeiter.Id);
            
        }


        //If Inputscan false Add


    }
    private void saveLieferscheinToDisplayList()
    {
        angezeigteLieferscheine.Add(new DisplayedModel { Lieferschein = _lieferscheinInput.Lieferschein, Nachname = _loggedInMitarbeiter.Nachname, TimeStamp = _lieferscheinInput.MontageTS });
    }

    private void mitarbeiterLogin(string mitarbeiterChipId)
    {
        //No LoggedIn
        if (_loggedInMitarbeiter == null)
        {
            _loggedInMitarbeiter = _sqlMa.GetMiarbeiterByChip(mitarbeiterChipId);

        }
        //Current User Logged in
        else if (_loggedInMitarbeiter != null && _loggedInMitarbeiter.ChipId == mitarbeiterChipId)
        {
            _loggedInMitarbeiter = null;

        }
        //different User logged in
        else if (_loggedInMitarbeiter != null && _loggedInMitarbeiter.ChipId != mitarbeiterChipId)
        {
            _loggedInMitarbeiter = _sqlMa.GetMiarbeiterByChip(mitarbeiterChipId);
        }
        displayMitarbeiterName(_loggedInMitarbeiter);
    }

    private void displayMitarbeiterName(MitarbeiterModel input)
    {
        if (input != null)
        {
            DisplayName.Text = $"{input.Vorname} {input.Nachname}";
        }
        else
        {
            DisplayName.Text = "Kein Mitarbeiter";
        }
    }

    private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            //Check for the Input if tis ChipID or LS
            if (inputTextBox.Text.inputCheckLieferschein())
            {
                if (_loggedInMitarbeiter == null)
                {
                    InvalidInputWarning();
                }
                else if (_loggedInMitarbeiter != null)
                {
                    saveLieferscheinToDb(inputTextBox.Text);
                    saveLieferscheinToDisplayList();
                    ValidinputWarning();
                }

            }
            else if (inputTextBox.Text.inputCheckChipId())
            {
                mitarbeiterLogin(inputTextBox.Text);
                ValidinputWarning();
            }
            else
            {
                InvalidInputWarning();
            }
        }



    }
    private void InvalidInputWarning()
    {
        inputTextBox.Clear();
        inputTextBox.Background = Brushes.Red;
        inputTextBox.Focus();
    }
    private void ValidinputWarning()
    {
        inputTextBox.Clear();
        inputTextBox.Background = Brushes.White;
        inputTextBox.Focus();
        DisplayName.Background = Brushes.White;
    }

}
