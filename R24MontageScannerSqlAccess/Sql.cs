using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace R24MontageScannerSqlAccess;


public class Sql
{
    public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
        connection.Close();
        return rows;
    }

    public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(sqlStatement, parameters);
        }
    }
}
