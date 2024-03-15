using SqlAccessLib.Models;
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

        public void AddMitarbeiter(MitarbeiterModel mitarbeiter)
        {
            string sql = "insert into dbo.Mitarbeiter (Vorname, Nachname, ChipId) values (@Vorname, @Nachname, @ChipId);";
            db.SaveData(sql, 
                new { mitarbeiter.Vorname, mitarbeiter.Nachname, mitarbeiter.ChipId },
                _connectionString);
        }

        public void UpdateChipId(MitarbeiterModel mitarbeiter)
        {
            string sql = "update dbo.Mitarbeiter set Vorname = @Vorname, Nachname = @Nachname, ChipId = @ChipId;";
            db.SaveData(sql, mitarbeiter, _connectionString);
        }

    }
}
