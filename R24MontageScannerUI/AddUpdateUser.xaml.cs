using MontageScanLib;
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
using R24MontageScannerSqlAccess;



namespace R24MontageScannerUI;


public partial class AddUpdateUser : Window
{

    private SqlMitarbeiter _sqlMa;
    private MitarbeiterModel displayedMa = new MitarbeiterModel();
    public AddUpdateUser(string connectionString)
    {
        InitializeComponent();
        _sqlMa = new SqlMitarbeiter(connectionString); 

    }

    private void ChipTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
           
        {
            if (ChipTextBox.Text.inputCheckChipId())
            {
                //Check if there is a Employee with this ChipId
                DisplayNameOfMitarbeiter(GetMitarbeiter(ChipTextBox.Text));
                //put Employee Name into the Vorname and Nachname TextBox
                //If no employee exist let user enter name into Vorname Nachname textbox
            }
            else
            {

                WrongInputAlarm(ChipTextBox);
            }

        }
    }



    private MitarbeiterModel GetMitarbeiter(string ChipId)
    {
        MitarbeiterModel output = null;
        try
        {
            output = _sqlMa.GetMiarbeiterByChip(ChipTextBox.Text);
        }
        catch 
        {

        }
        return output;
    }

    private void DisplayNameOfMitarbeiter(MitarbeiterModel input)
    {
        //Display Textbox
        vorNameTextBox.Visibility = Visibility.Visible;
        nachNameTextBox.Visibility= Visibility.Visible;

        if (input == null)
        {
            vorNameTextBox.Focus();
            addButton.Visibility = Visibility.Visible;
        }
        else
        {
            vorNameTextBox.Text = input.Vorname;
            nachNameTextBox.Text = input.Nachname;
            updateButton.Focus();
            updateButton.Visibility = Visibility.Visible;
        }
    }


    private void BtnClick_Update(object sender, RoutedEventArgs e)
    {
        MitarbeiterModel input = new MitarbeiterModel { Vorname = vorNameTextBox.Text, Nachname = nachNameTextBox.Text, ChipId = ChipTextBox.Text };
        if (input.validMitarbeiterInput())
        {
            _sqlMa.UpdateMitarbeiterNameByChipId(input);
        }
        resetUI(this);

    }

    private void BtnClick_Add(object sender, RoutedEventArgs e)
    {
        MitarbeiterModel input = new MitarbeiterModel { Vorname = vorNameTextBox.Text, Nachname = nachNameTextBox.Text, ChipId = ChipTextBox.Text };
        _sqlMa.AddMiarbeiter(input);
        resetUI(this);
    }

    private void resetUI(object sender)
    {
        updateButton.Visibility = Visibility.Collapsed;
        addButton.Visibility = Visibility.Collapsed;
        vorNameTextBox.Visibility = Visibility.Collapsed;
        nachNameTextBox.Visibility = Visibility.Collapsed;
        vorNameTextBox.Clear();
        nachNameTextBox.Clear();
        ChipTextBox.Clear();
        ChipTextBox.Focus();
        ChipTextBox.Background = Brushes.White;
        vorNameTextBox.Clear();
        nachNameTextBox.Clear();
        
    }












    private void WrongInputAlarm(TextBox sender)
    {
        sender.Background = Brushes.Red;
        sender.Clear();
        sender.Focus();
    }

    private void BtnClick_Close(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
