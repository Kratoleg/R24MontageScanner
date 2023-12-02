using CsvHelper.Configuration.Attributes;



namespace MontageScanLib
{

    public class CSVReadMontageLieferscheinModel
    {
        //L123456,Status,EingangsDatum,MontageDatum,VersandDatum,Mitarbeiter;

        [Name("Lieferschein")]
        public string Lieferschein { get; set; }

        [Name("Status")]
        public LieferscheinStatus.LsStatus Status { get; set; }

        [Name("EingangsTimeStamp")]
        public DateTime EingangsTimeStamp { get; set; }

        //[Name("Montage")]
        //public DateTime MontageTimeStamp { get; set; }

        //[Name("Versand")]
        //public DateTime VersandTimeStamp { get; set; }

        //[Name("Mitarbeiter")]
        //public MitarbeiterModel Monteur { get; set; }

    }


}
