
using R24MontageScannerSqlAccess.Models;

namespace R24MontageScannerSqlAccess;

public class SqlLieferschein
{
    private readonly string _connectionString;
    private Sql dbAccess = new Sql();

    public SqlLieferschein(string connectionstring)
    {
        _connectionString = connectionstring;
    }

    public void LieferscheinEingangsScan(EingangsLieferscheinModel input)
    {
        if (InputLsCheck(input) == true)
        {
            string command = "insert into dbo.Vorgang (Lieferschein, EingangsTS) values (@Lieferschein,  @EingangsTS);";

            dbAccess.SaveData(command,
        new { input.Lieferschein, input.EingangsTS },
        _connectionString);
        }
        else
        {
            throw new Exception("no valid input");
        }

    }
    public void UpdateLieferschein(EingangsLieferscheinModel input)
    {
        if (InputLsCheck(input) == true)
        {
            string command = "update dbo.Vorgang set EingangsTS = @EingangsTS where Lieferschein = @Lieferschein ;";

            dbAccess.SaveData(command,
        new { input.Lieferschein, input.EingangsTS },
        _connectionString);
        }
        else
        {
            throw new Exception("no valid input");
        }
    }

    public void LieferscheinMontageScan(MontageLieferscheinModel input, int MonteurId)
    {
        //I was about to check the Values before
        string command = "update dbo.Vorgang set MontageTS = @MontageTS, MitarbeiterId= @MonteurId where Lieferschein = @Lieferschein;";
        dbAccess.SaveData(command, new { input.Lieferschein, input.MontageTS, MonteurId }, _connectionString);
    }

    public searchLieferschein SucheNachLieferschein(string lieferschein)
    {
        searchLieferschein? output = new();
        string command = "select Id, Lieferschein, EingangsTS, MontageTS, MitarbeiterId from dbo.Vorgang where Lieferschein = @lieferschein;";
        output = dbAccess.LoadData<searchLieferschein, dynamic>(command, new { lieferschein }, _connectionString).FirstOrDefault();

        if (output == null)
        {
            throw new Exception("Lieferschein nciht gefunden");
        }
        else { return output; }

    }

    private bool InputLsCheck(EingangsLieferscheinModel input)
    {
        bool output = false;
        string tempDELETEME = input.EingangsTS.Year.ToString();
        string CurrentTime = DateTime.Now.Year.ToString();
        string futureTime = DateTime.Now.AddYears(1).Year.ToString();

        if (input.Lieferschein.Length == 7 && (input.EingangsTS.AddYears(-1).Year == DateTime.Now.Year || input.EingangsTS.Year == DateTime.Now.Year))
        {
            output = true;
        }
        return output;
    }
}
