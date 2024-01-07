using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlAccessLib
{
    public class SqlMitarbeiter
    {
        private readonly string _connectionString;
        private SqlAccess db = new SqlAccess();
        public SqlMitarbeiter( string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddMitarbeiter()
        {
            //Take in MitarbeiterModel and save to Db
        }



    }
}
