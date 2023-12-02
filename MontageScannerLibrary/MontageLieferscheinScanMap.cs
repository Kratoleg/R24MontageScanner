using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace MontageScanLib
{
    public class MontageLieferscheinScanMap : ClassMap<MontageLieferscheinModel>
    {
        public MontageLieferscheinScanMap()
        {
            Map(m => m.Lieferschein).Name("Lieferschein");
            Map(m => m.Status).Name("Status");
            Map(m => m.EingangsTimeStamp).Name("EingangsTimeStamp");
            //Map(m => m.MontageTimeStamp).Name("Montage");
            //Map(m => m.VersandTimeStamp).Name("Versand");
            //References<MitarbeiterModelMap>(m => m.Monteur, "Mitarbeiter");

        }
    }



    public class MontageLieferscheinModel
    {
        //L123456,Status,EingangsDatum,MontageDatum,VersandDatum,Mitarbeiter;

        public string Lieferschein { get; set; }

        public LieferscheinStatus.LsStatus Status { get; set; }

        public DateTime EingangsTimeStamp { get; set; }

        //public DateTime MontageTimeStamp { get; set; }
        //public DateTime VersandTimeStamp { get; set; }
        //public MitarbeiterModel Monteur { get; set; }



        public MontageLieferscheinModel(string lieferSchein)
        {
            Lieferschein = lieferSchein;
            Status = LieferscheinStatus.LsStatus.kommissionierung;
            EingangsTimeStamp = DateTime.Now;
        }
        public string StringEingangsTimeStamp
        { get { return EingangsTimeStamp.ToString("dd.MM.yyyy HH:mm:ss"); } }

    }


}
