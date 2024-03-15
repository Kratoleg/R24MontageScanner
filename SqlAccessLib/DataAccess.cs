using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SqlAccessLib
{
    internal class DataAccess
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
}
