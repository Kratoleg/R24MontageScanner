
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
            string command = "insert indo dbo.Vorgang (Lieferschein, EingangsTS) values (@Lieferscheinm  @EingangsTS);";
            dbAccess.SaveData(command,
        new { input.Lieferschein, input.EingangsTS },
        _connectionString);
        }
        else
        {
            throw new Exception("no valid input");
        }
        
    }

    private bool InputLsCheck(EingangsLieferscheinModel input)
    {
        bool output = false;
        if(input.Lieferschein.Length == 7 && input.EingangsTS.Year == DateTime.Now.Year)
        {
            output = true;
        }
        return output;
    }
}
