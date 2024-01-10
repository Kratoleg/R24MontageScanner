using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace R24MontageScannerSqlAccess;


public class SqlMitarbeiter
{
    private readonly string _connectionString;
    private Sql dbAccess = new Sql();

    public SqlMitarbeiter(string connectionstring)
    {
        _connectionString = connectionstring;
    }


    public void AddMiarbeiter(MitarbeiterModel mitarbeiter)
    {
        string command = "insert into dbo.Mitarbeiter (Vorname, Nachname, ChipId) values (@Vorname, @Nachname, @ChipId);";
        dbAccess.SaveData(command,
    new { mitarbeiter.Vorname, mitarbeiter.Nachname, mitarbeiter.ChipId },
    _connectionString);
    }

    public void UpdateMitarbeiterName(MitarbeiterModel mitarbeiter)
    {
        string command = "update dbo.Mitarbeiter set Vorname = @Vorname, Nachname = @Nachname where ChipId = @ChipId;";
        dbAccess.SaveData(command, mitarbeiter, _connectionString);
    }
    public MitarbeiterModel GetMiarbeiterByChip(string ChipId)
    {
        string command = "select Id, Vorname, Nachname from dbo.Mitarbeiter where ChipId = @ChipId;";
        MitarbeiterModel output = dbAccess.LoadData<MitarbeiterModel, dynamic>(command, new { ChipId }, _connectionString).FirstOrDefault();
      
        if (checkForValidModel(output) == true)
        {
            return output;
        }
        else
        {
            throw new Exception( "Mitarbeiter nicht funden");
        }

    }

    private bool checkForValidModel(MitarbeiterModel valueToCheck)
    {
        bool output = false;

        if(valueToCheck.ChipId != null && valueToCheck.Vorname != null && valueToCheck.Nachname != null)
        {
           if(valueToCheck.ChipId.Length > 0 && valueToCheck.Vorname.Length > 0 && valueToCheck.Nachname.Length > 0) 
            {
                output = true;
            }
            
        }
        return output;
    }
}
