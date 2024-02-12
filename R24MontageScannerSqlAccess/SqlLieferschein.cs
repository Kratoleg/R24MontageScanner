
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
            string command = "insert into dbo.Vorgang (Lieferschein, EingangsTS) values (@Lieferschein  @EingangsTS);";
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
        string command = "update dbo.Vorgang set (MontageTS, MitarbeiterId) values (@MontageTS, @MonteurId) where where Lieferschein = @Lieferschein ;";
        dbAccess.SaveData(command, new { input.MontageTS, MonteurId }, _connectionString);
    }



private bool InputLsCheck(EingangsLieferscheinModel input)
{
    bool output = false;
    if (input.Lieferschein.Length == 6 && (input.EingangsTS.Year + 1 == DateTime.Now.Year || input.EingangsTS.Year == DateTime.Now.Year))
    {
        output = true;
    }
    return output;
}
}
